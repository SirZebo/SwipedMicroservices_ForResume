using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bidding.Infrastructure.Data.Extensions;
public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);
        await SeedAuctionAsync(context);
        await SeedBidAsync(context);
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedAuctionAsync(ApplicationDbContext context)
    {
        if (!await context.Auctions.AnyAsync())
        {
            await context.Auctions.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedBidAsync(ApplicationDbContext context)
    {
        if (!await context.Bids.AnyAsync())
        {
            await context.Bids.AddRangeAsync(InitialData.Bids);
            await context.SaveChangesAsync();
        }
    }

}
