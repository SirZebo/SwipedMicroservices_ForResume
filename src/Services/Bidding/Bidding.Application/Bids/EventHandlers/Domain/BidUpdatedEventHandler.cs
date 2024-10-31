
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;


namespace Bidding.Application.Bids.EventHandlers.Domain;
public class BidUpdatedEventHandler
    (IPublishEndpoint publishEndpoint,
    ILogger<BidUpdatedEventHandler> logger)
    : INotificationHandler<BidUpdatedEvent>
{
    public async Task Handle(BidUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var bidUpdatedIntegrationEvent = MapToIntegrationEvent(domainEvent);

        await publishEndpoint.Publish(bidUpdatedIntegrationEvent, cancellationToken);
    }

    private BidUpdatedIntegrationEvent MapToIntegrationEvent(BidUpdatedEvent domainEvent)
    {
        return new BidUpdatedIntegrationEvent
        {
            BidId = domainEvent.Bid.Id.Value,
            CustomerId = domainEvent.Bid.CustomerId.Value,
            Price = domainEvent.Bid.Price
        };
    }
}
