namespace Bidding.Domain.Events;
public record BidCreatedEvent(Bid Bid) : IDomainEvent;
