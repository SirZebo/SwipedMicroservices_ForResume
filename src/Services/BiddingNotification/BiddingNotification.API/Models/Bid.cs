namespace BiddingNotification.API.Models;

public class Bid
{
    public Guid BidId { get; set; } = default!;
    public Guid AuctionId { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
