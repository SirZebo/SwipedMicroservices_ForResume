using Bidding.API;
using Bidding.Application;
using Bidding.Infrastructure;
using Bidding.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline automatically instead of DI in every time a new endpoint is created
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();