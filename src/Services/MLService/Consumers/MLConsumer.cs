using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MLService.Consumers
{
    public class MLConsumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _inputQueueName = "ProductCategorizationQueue";
        private readonly string _outputQueueName = "MLResultsQueue";

        public MLConsumer()
        {
            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RabbitMQ__HostName") ?? "localhost",
                Port = int.Parse(Environment.GetEnvironmentVariable("RabbitMQ__Port") ?? "5672")
            };
            int maxRetries = 5;
            int retryCount = 0;
            while (retryCount < maxRetries)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    _channel = _connection.CreateModel();
                    Console.WriteLine("Successfully connected to RabbitMQ.");  
                    break;  
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException)
                {
                    retryCount++;
                    Console.WriteLine($"Attempt {retryCount} - RabbitMQ not reachable, retrying in 5 seconds...");
                    Thread.Sleep(5000); 
                }
            }

            if (_connection == null || _channel == null)
                throw new Exception("Could not establish connection to RabbitMQ after multiple attempts.");

            _channel.QueueDeclare(queue: _inputQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: _outputQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var mlRequest = JsonSerializer.Deserialize<MLRequest>(message);

                Console.WriteLine($"Received message from RabbitMQ: {message}");

                if (mlRequest != null)
                {
                    var result = ProcessMLTask(mlRequest);

                    Console.WriteLine($"Processed result: {JsonSerializer.Serialize(result)}");

                    PublishResult(result);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: _inputQueueName, autoAck: false, consumer: consumer);
        }

        private MLResult ProcessMLTask(MLRequest request)
        {
            var categories = new List<string> { "Category1", "Category2" };
            return new MLResult { RequestId = request.RequestId, Categories = categories, Status = "Processed" };
        }

        private void PublishResult(MLResult result)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));
            _channel.BasicPublish(exchange: "", routingKey: _outputQueueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }

    public record MLRequest
    {
        public Guid RequestId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public record MLResult
    {
        public Guid RequestId { get; set; }
        public List<string> Categories { get; set; } = new();
        public string Status { get; set; } = "Processed";
    }
}
