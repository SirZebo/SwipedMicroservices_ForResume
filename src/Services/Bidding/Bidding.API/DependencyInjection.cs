using BuildingBlocks.Exceptions.Handler;
using Carter;

namespace Bidding.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();
        //services.AddHealthChecks()
        //    .AddSqlServer(configuration.GetConnectionString("Database")!);
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500",
                                        "http://localhost",
                                        "https://localhost");
                });
        });

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors();
        app.MapCarter();

        app.UseExceptionHandler(options => { });
        //app.UseHealthChecks("/health",
        //    new HealthCheckOptions
        //    {
        //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //    });

        return app;
    }
}