using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MLService.ML;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register the ML model as a singleton for dependency injection
builder.Services.AddSingleton<ProductCategorizationModel>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Provides detailed error info in development
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); // Maps controller endpoints, like /api/ml/predict

app.Run("http://0.0.0.0:8080");
