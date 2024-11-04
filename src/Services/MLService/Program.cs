using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MLService.Data;
using MLService.ML;
using MLService.Repositories;
using MLService.Consumers; // Import the RabbitMQ consumer
using System;
using k8s;
using k8s.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services with the container
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 40))));

// Register ProductCategorizationModel as a singleton for DI
builder.Services.AddSingleton<ProductCategorizationModel>();

// Register IPredictionRepository and PredictionRepository for DI
builder.Services.AddScoped<IPredictionRepository, PredictionRepository>();

// Register the Kubernetes client
builder.Services.AddSingleton<Kubernetes>(sp =>
{
    // Use InClusterConfig for Kubernetes or BuildConfigFromConfigFile for local development
    var config = KubernetesClientConfiguration.IsInCluster() 
        ? KubernetesClientConfiguration.InClusterConfig() 
        : KubernetesClientConfiguration.BuildConfigFromConfigFile();
    return new Kubernetes(config);
});


// Build the app
var app = builder.Build();

// Test the database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.OpenConnection();
        Console.WriteLine("Database connection is successful!");
        dbContext.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

// Initialize and start the RabbitMQ consumer for ML tasks
var mlConsumer = new MLConsumer();
app.Lifetime.ApplicationStarted.Register(() => mlConsumer.StartListening());
app.Lifetime.ApplicationStopping.Register(() => mlConsumer.Dispose());

// Configure middleware and routing
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Run the app on specified URL
app.Run("http://0.0.0.0:8080");
