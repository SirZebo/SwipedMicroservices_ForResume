namespace Bidding.Domain.Events;
public record BidUpdatedEvent(Bid Order) : IDomainEvent;

