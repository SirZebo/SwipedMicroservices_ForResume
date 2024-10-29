namespace BiddingResult.API.Models;

public class Contract
{
    public Guid Id { get; set; }
    public Guid AuctionId { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal Price {  get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.NotPaid;

}

