namespace BuildingBlocks.Messaging.Events;
public record BidUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid BidId { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
