using BiddingNotification.API.EventHandler;
using MassTransit;
using System.Reflection;

namespace BiddingNotification.API.Data.Extensions;

public static class Extensions
{
    private static string instanceId = "Notification";
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {

        services.AddMassTransit(config =>
        {
            // Set naming convention for endpoints
            config.SetKebabCaseEndpointNameFormatter();

            config.AddConsumer<OutbidEventHandler>()
                .Endpoint(e => e.InstanceId = Guid.NewGuid().ToString()); // Each instance is unique

            config.AddConsumer<BidUpdatedEventHandler>()
                .Endpoint(e => e.InstanceId = instanceId);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]);
                    host.Password(configuration["MessageBroker:Password"]);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
