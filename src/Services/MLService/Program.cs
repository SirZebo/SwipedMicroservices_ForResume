using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MLService.Data;
using MLService.ML;  // Ensure namespace for ProductCategorizationModel is correct
using MLService.Repositories;
using System;

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

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run("http://0.0.0.0:8080");
