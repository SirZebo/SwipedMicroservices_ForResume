using System.Collections.Concurrent;

namespace Bidding.Infrastructure.Data;
public class CachedBiddingRepository
    (IBiddingRepository repository)
    : IBiddingRepository
{
    public static ConcurrentDictionary<AuctionId, Bid> cache = new();

    public async Task<Bid> GetHighestBid(AuctionId id, CancellationToken cancellationToken = default)
    {
        Bid cachedBid;
        if (cache.TryGetValue(id, out cachedBid)) 
        { 
            return cachedBid; 
        }
        
        var bid = await repository.GetHighestBid(id, cancellationToken);
        cache[id] = bid;
        return bid;
    }

    public async Task<Bid> StoreBid(Bid bid, CancellationToken cancellationToken = default)
    {
        await repository.StoreBid(bid, cancellationToken);
        cache[bid.AuctionId] = bid;

        return bid;
    }
}
