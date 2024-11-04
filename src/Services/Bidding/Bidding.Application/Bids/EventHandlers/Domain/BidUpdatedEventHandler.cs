
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;


namespace Bidding.Application.Bids.EventHandlers.Domain;
public class BidUpdatedEventHandler
    (IPublishEndpoint publishEndpoint,
    ILogger<BidUpdatedEventHandler> logger)
    : INotificationHandler<Bidding.Domain.Events.BidUpdatedEvent>
{
    public async Task Handle(Bidding.Domain.Events.BidUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var bidUpdatedIntegrationEvent = MapToIntegrationEvent(domainEvent);

        await publishEndpoint.Publish(bidUpdatedIntegrationEvent, cancellationToken);
    }

    private BuildingBlocks.Messaging.Events.BidUpdatedEvent MapToIntegrationEvent(Bidding.Domain.Events.BidUpdatedEvent domainEvent)
    {
        return new BuildingBlocks.Messaging.Events.BidUpdatedEvent
        {
            BidId = domainEvent.Bid.Id.Value,
            AuctionId = domainEvent.Bid.AuctionId.Value,
            CustomerId = domainEvent.Bid.CustomerId.Value,
            Price = domainEvent.Bid.Price
        };
    }
}
