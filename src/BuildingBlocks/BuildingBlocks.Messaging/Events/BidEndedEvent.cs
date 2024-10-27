namespace BuildingBlocks.Messaging.Events;
public record BidEndedEvent : IntegrationEvent
{
    public Guid BidId { get; set; }
    public Guid AuctionId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Price { get; set; }
}
