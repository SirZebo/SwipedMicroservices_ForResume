using BiddingNotification.API.Hubs;
using BiddingNotification.API.Models;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BiddingNotification.API.EventHandler;

public class OutbidEventHandler
    (IBiddingHub hub,
    ILogger<OutbidEventHandler> logger)
    : IConsumer<OutbidEvent>
{
    public async Task Consume(ConsumeContext<OutbidEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var bid = MapToBid(context.Message);
        await hub.SendSseByAuctionId(bid);
    }
    private Bid MapToBid(OutbidEvent message)
    {
        return new Bid
        {
            Id = message.BidId,
            AuctionId = message.AuctionId,
            CustomerId = message.CustomerId,
            Price = message.Price
        };
    }
}
