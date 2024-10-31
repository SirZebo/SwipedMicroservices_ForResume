namespace Bidding.Domain.Events;
public record BidUpdatedEvent(Bid Bid) : IDomainEvent;

