namespace BuildingBlocks.Messaging.Events;

public record AuctionEndedEvent : IntegrationEvent
{
    public Guid AuctionId { get; set; }
}
