using MassTransit;
using System.Reflection;

namespace JobScheduler.API.Extensions;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            // Set naming convention for endpoints
            config.SetKebabCaseEndpointNameFormatter();

            config.AddPublishMessageScheduler();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.UsePublishMessageScheduler();
                configurator.UseHangfireScheduler();
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