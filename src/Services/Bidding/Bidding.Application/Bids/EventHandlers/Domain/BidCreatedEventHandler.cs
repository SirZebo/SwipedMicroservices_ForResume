
using Bidding.Domain.ValueObjects;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;

namespace Bidding.Application.Bids.EventHandlers.Domain;
public class BidCreatedEventHandler
    (IPublishEndpoint publishEndpoint,
    ILogger<BidCreatedEventHandler> logger)
    : INotificationHandler<BidCreatedEvent>
{
    public async Task Handle(BidCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var bidCreatedIntegrationEvent = MapToIntegrationEvent(domainEvent);

        await publishEndpoint.Publish(bidCreatedIntegrationEvent, cancellationToken);
    }

    private BidCreatedIntegrationEvent MapToIntegrationEvent(BidCreatedEvent domainEvent)
    {
        return new BidCreatedIntegrationEvent
        {
            BidId = domainEvent.Bid.Id.Value,
            AuctionId = domainEvent.Bid.AuctionId.Value,
            CustomerId = domainEvent.Bid.CustomerId.Value,
            Price = domainEvent.Bid.Price
        };
    }
}
