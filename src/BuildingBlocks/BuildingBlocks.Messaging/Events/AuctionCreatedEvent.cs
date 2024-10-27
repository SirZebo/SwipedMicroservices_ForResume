namespace BuildingBlocks.Messaging.Events;
public record AuctionCreatedEvent : IntegrationEvent
{
    public Guid AuctionId { get; set; }
    public string Name { get; set; } = default!;
    public DateTime EndingDate { get; set; }
    public decimal StartingPrice { get; set; }
}
