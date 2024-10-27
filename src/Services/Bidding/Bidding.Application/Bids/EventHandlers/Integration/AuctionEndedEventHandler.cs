using Bidding.Domain.Abstractions;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Bidding.Application.Bids.EventHandlers.Integration;
public class AuctionEndedEventHandler
    (IPublishEndpoint publisher,
    IApplicationDbContext dbContext,
    ILogger<AuctionEndedEventHandler> logger)
    : IConsumer<AuctionEndedEvent>
{
    public async Task Consume(ConsumeContext<AuctionEndedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var auctionId = AuctionId.Of(context.Message.AuctionId);
        var bid = await dbContext.Bids.Where(b => b.AuctionId == auctionId).FirstOrDefaultAsync();

        if (bid is null || bid.CustomerId.Value == Guid.Empty)
        {
            return; // No one bidded
        }

        var bidEndedIntegrationEvent = MapToBidEndedIntegrationEvent(bid);

        await publisher.Publish(bidEndedIntegrationEvent);
    }

    private object MapToBidEndedIntegrationEvent(Bid bid)
    {
        return new BidEndedEvent
        {
            BidId = bid.Id.Value,
            AuctionId = bid.AuctionId.Value,
            CustomerId = bid.CustomerId.Value,
            Price = bid.Price,
        };
    }
}
