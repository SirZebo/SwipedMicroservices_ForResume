using Bidding.Application.Data;
using Bidding.Application.Exceptions;

namespace Bidding.Infrastructure.Data;
public class BiddingRepository
    (IApplicationDbContext dbContext)
    : IBiddingRepository
{
    public async Task<Bid> GetHighestBid(AuctionId id, CancellationToken cancellationToken = default)
    {
        var highestPrice = await dbContext.Bids
            .Where(a => a.AuctionId == id)
            .MaxAsync(b => b.Price, cancellationToken);
        var highestBid = await dbContext.Bids
            .Where(a => a.AuctionId == id && a.Price == highestPrice)
            .FirstAsync(cancellationToken);

        return highestBid is null ? throw new AuctionNotFoundException(id.Value) : highestBid;
    }

    public async Task<Bid> StoreBid(Bid bid, CancellationToken cancellationToken = default)
    {
        dbContext.Bids.Update(bid);
        await dbContext.SaveChangesAsync(cancellationToken);
        return bid;
    }
}
