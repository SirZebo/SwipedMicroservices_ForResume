namespace BuildingBlocks.Messaging.Events;

public record AuctionCategorisedEvent : IntegrationEvent
{
    public Guid AuctionId { get; set; }
    public required List<string> Category { get; set; }
}
