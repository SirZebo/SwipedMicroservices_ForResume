
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Auction.API.Data;

public class CachedAuctionRepository
    (IAuctionRepository repository, IDistributedCache cache)
    : IAuctionRepository
{
    //public Task<bool> DeleteAuction(Guid id, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Models.Auction> GetAuctionById(Guid id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"auction:{id}";
        var cachedAuction = await cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedAuction)) 
            return JsonSerializer.Deserialize<Models.Auction>(cachedAuction)!;

        var auction = await repository.GetAuctionById(id, cancellationToken);
        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(auction), cancellationToken);
        return auction;
    }

    public async Task<IEnumerable<Models.Auction>> GetAuctionByCategory(string category, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"auction:{category}";
        var cachedAuction = await cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedAuction))
            return JsonSerializer.Deserialize<IEnumerable<Models.Auction>>(cachedAuction)!;

        var auction = await repository.GetAuctionByCategory(category, cancellationToken);
        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(auction), cancellationToken);
        return auction;
    }

    public async Task<IEnumerable<Models.Auction>> GetAuctions(CancellationToken cancellationToken = default)
    {
        var cacheKey = $"auctions";
        var cachedAuction = await cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedAuction))
            return JsonSerializer.Deserialize<IEnumerable<Models.Auction>>(cachedAuction)!;

        var auction = await repository.GetAuctions(cancellationToken);
        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(auction), cancellationToken);
        return auction;
    }

    public async Task<Models.Auction> StoreAuction(Models.Auction auction, CancellationToken cancellationToken = default)
    {
        await repository.StoreAuction(auction, cancellationToken);

        var cacheKey = "auctions";
        await cache.RemoveAsync(cacheKey, cancellationToken);

        foreach (var category in auction.Category)
        {
            cacheKey = $"auction:{category}";
            await cache.RemoveAsync(cacheKey, cancellationToken);
        }

        return auction;
    }
}

