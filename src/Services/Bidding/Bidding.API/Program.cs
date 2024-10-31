using Bidding.API;
using Bidding.Application;
using Bidding.Infrastructure;
using Bidding.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

// Initialize the hash ring with some nodes
var nodes = new List<string> { "BidService-1", "BidService-2", "BidService-3" };
var hashRing = new ConsistentHashRing(nodes);

// Configure the HTTP request pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

// Endpoint to assign a node based on auction ID
app.MapPost("/assign_node", (HttpContext context) =>
{
    var auctionId = context.Request.Form["auction_id"].ToString();
    var assignedNode = hashRing.GetNode(auctionId);
    return Results.Json(new { auction_id = auctionId, assigned_node = assignedNode });
});

// Endpoint to add a new node to the hash ring
app.MapPost("/add_node", (HttpContext context) =>
{
    var node = context.Request.Form["node"].ToString();
    hashRing.AddNode(node);
    return Results.Text($"Node {node} added to the ring.");
});

// Endpoint to remove a node from the hash ring
app.MapPost("/remove_node", (HttpContext context) =>
{
    var node = context.Request.Form["node"].ToString();
    hashRing.RemoveNode(node);
    return Results.Text($"Node {node} removed from the ring.");
});

// Run the application
app.Run();
