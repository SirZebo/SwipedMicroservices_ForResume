namespace Bidding.Domain.Models;
public class Bid : Aggregate<BidId>
{
    public AuctionId AuctionId { get; private set; } = default!;
    public CustomerId CustomerId { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Bid Create(BidId id, AuctionId auctionId, CustomerId customerId, decimal price)
    {
        var bid = new Bid
        {
            Id = id,
            CustomerId = customerId,
            AuctionId = auctionId,
            Price = price
        };

        bid.AddDomainEvent(new BidCreatedEvent(bid));

        return bid;
    }

}
