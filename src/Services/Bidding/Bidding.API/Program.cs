using Bidding.API;
using Bidding.Application;
using Bidding.Infrastructure;
using Bidding.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using k8s;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration)
    .AddSingleton<LeaderElection>()
    .AddSingleton<IKubernetes>(serviceProvder => 
    {
        var config = KubernetesClientConfiguration.InClusterConfig();
        return new Kubernetes(config);
    });

var app = builder.Build();

//get required services
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var leaderElection = app.Services.GetRequiredService<LeaderElection>();

// Initialize the hash ring with some nodes
var nodes = new List<string> { "BidService-1", "BidService-2", "BidService-3" };
var hashRing = new ConsistentHashRing(nodes);

_= Task.Run(async () => 
{
    await leaderElection.StartAsync(
        onBecameLeader: () => 
        {
            logger.LogInformation("This instance became the leader");
            //add any leader-specific initialisation here
        },
        onLostLeadership: () => 
        {
            logger.LogInformation("This instance is no longer the leader");
        }
    );
});

// Configure the HTTP request pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

// Endpoint to assign a node based on auction ID from JSON
app.MapPost("/assign_node", async (HttpContext context) =>
{
    if(!leaderElection.IsLeader) {
        return Results.StatusCode(503);
    }
    // Read and deserialize the JSON body
    var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
    var data = JsonSerializer.Deserialize<Dictionary<string, string>>(requestBody);

    // Retrieve auction_id if present, otherwise return BadRequest
    if (data != null && data.ContainsKey("auction_id"))
    {
        var auctionId = data["auction_id"];
        var assignedNode = hashRing.GetNode(auctionId);
        return Results.Json(new { auction_id = auctionId, assigned_node = assignedNode });
    }
    else
    {
        return Results.BadRequest("Required parameter 'auction_id' is missing.");
    }
});

// Endpoint to add a new node to the hash ring
app.MapPost("/add_node", async (HttpContext context) =>
{
    if (!leaderElection.IsLeader) {
        return Results.StatusCode(503);
    }
    var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
    var jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(requestBody);
    var node = jsonData["node"];
    hashRing.AddNode(node);
    return Results.Text($"Node {node} added to the ring.");
});

// Endpoint to remove a node from the hash ring
app.MapPost("/remove_node", async (HttpContext context) =>
{
    
    if (!leaderElection.IsLeader) {
        return Results.StatusCode(503);
    }
    var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
    var jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(requestBody);
    var node = jsonData["node"];
    hashRing.RemoveNode(node);
    return Results.Text($"Node {node} removed from the ring.");
});

app.MapGet("/health", () => 
{
    return Results.Json(new 
    { 
        isLeader = leaderElection.IsLeader,
        status = "healthy" 
    });
});
// Run the application
app.Run();
