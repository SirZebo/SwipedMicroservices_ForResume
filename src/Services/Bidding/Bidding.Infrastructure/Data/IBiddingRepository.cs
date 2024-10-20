using Bidding.Domain.Models;

namespace Bidding.Infrastructure.Data;
public interface IBiddingRepository
{
    Task<Bid> GetHighestBid(AuctionId id, CancellationToken cancellationToken = default);

    Task<Bid> StoreBid(Bid bid, CancellationToken cancellationToken = default);
}
