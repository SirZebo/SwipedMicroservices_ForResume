// File: ProductCategorizationService/Consumers/AuctionCreatedConsumer.cs

using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Auction.API.Models; // Ensures access to Auction class in Auction.API

namespace ProductCategorizationService.Consumers
{
    public class AuctionCreatedConsumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "AuctionCreatedQueue";

        public AuctionCreatedConsumer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var auctionEvent = JsonSerializer.Deserialize<Auction.API.Models.Auction>(message); // Fully qualified to avoid conflicts

                if (auctionEvent != null)
                {
                    ProcessAuctionEvent(auctionEvent);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: _queueName,
                                  autoAck: false,
                                  consumer: consumer);
        }

        private void ProcessAuctionEvent(Auction.API.Models.Auction auctionEvent) // Fully qualified to avoid conflicts
        {
            var categories = CategorizeAuctionItem(auctionEvent);
            PublishCategorizedEvent(new AuctionCategorisedEvent
            {
                AuctionId = auctionEvent.Id,
                Category = categories
            });
        }

        private List<string> CategorizeAuctionItem(Auction.API.Models.Auction auctionEvent) // Fully qualified
        {
            // Placeholder for ML categorization logic
            return new List<string> { "Category1", "Category2" };
        }

        private void PublishCategorizedEvent(AuctionCategorisedEvent categorizedEvent)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(categorizedEvent));
            _channel.BasicPublish(exchange: "",
                                  routingKey: "AuctionCategorisedQueue",
                                  basicProperties: null,
                                  body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }

    public record AuctionCategorisedEvent
    {
        public Guid AuctionId { get; set; }
        public List<string> Category { get; set; } = new();
    }
}
