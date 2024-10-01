using BuildingBlocks.Behaviors;
using Carter;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container. - builder.AddServices()
builder.Services.AddCarter();
// Automatically does DI of our handlers through assembly reflection
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline automatically instead of DI in every time a new endpoint is created
app.MapCarter();

app.Run();
