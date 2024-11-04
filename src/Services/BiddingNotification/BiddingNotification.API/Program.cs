using BiddingNotification.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

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
app.MapHub<BiddingHub>("/bidding/notification/{id}");

app.Run();
