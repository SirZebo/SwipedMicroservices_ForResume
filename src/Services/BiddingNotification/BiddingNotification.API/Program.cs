using BiddingNotification.API.Data;
using BiddingNotification.API.Data.Extensions;
using BiddingNotification.API.Hubs;
using BuildingBlocks.Behaviors;
using FluentValidation;
using Marten;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddSignalR();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<BidInitialData>();

builder.Services.AddScoped<IBiddingHub, BiddingHub>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000");
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapGet("/", () => "Hello World!");
app.MapHub<BiddingHub>("/bidding/notification/{auctionId}");

app.Run();
