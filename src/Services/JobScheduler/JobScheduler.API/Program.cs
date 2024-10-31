using BuildingBlocks.Behaviors;
using Hangfire;
using JobScheduler.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using BuildingBlocks.Exceptions.Handler;
using JobScheduler.API.Hangfire;
using JobScheduler.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Automatically does DI of our handlers through assembly reflection
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(connectionString)
);

builder.Services.AddHangfire(x =>
    x.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Database")!)
);

builder.Services.AddHangfireServer(x => x.SchedulePollingInterval = TimeSpan.FromSeconds(15));

// Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("Database")!);

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new MyAuthorizationFilter() }
});

app.UseExceptionHandler(options => { });

app.MapGet("/job", (IBackgroundJobClient jobClient) =>
{
    jobClient.Schedule(() => Console.WriteLine("Hello"), TimeSpan.FromSeconds(5));
    return Results.Ok("Job Created!");
});

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });


app.Run();


