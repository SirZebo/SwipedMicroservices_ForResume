using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

public class RabbitMQService
{
    private readonly string _hostname = "localhost";
    private readonly string _username = "guest";
    private readonly string _password = "guest";

    public void ConnectAndListen()
    {
        var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "BuildingBlocks.Messaging.Events:AuctionCreatedEvent", type: "fanout");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: "BuildingBlocks.Messaging.Events:AuctionCreatedEvent", routingKey: "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received: {0}", message);
                // TODO: Add logic to process the auction event here
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            Console.WriteLine("Waiting for messages. To exit press CTRL+C");
            Console.ReadLine();
        }
    }
}
