namespace Bidding.Domain.Models;
public class Bid : Aggregate<BidId>
{
    public AuctionId AuctionId { get; set; }
    public CustomerId CustomerId { get; set; }
    public decimal Price { get; set; }

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
