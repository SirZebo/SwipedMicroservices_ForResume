using Auction.API.Auctions.GetAuctionById;
using Auction.API.Auctions.UpdateAuction;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Auction.API.Auctions.EventHandler.Integration;

public class AuctionCategorisedHandler
    (ILogger<AuctionCategorisedHandler> logger,
    ISender sender)
    : IConsumer<AuctionCategorisedEvent>
{
    public async Task Consume(ConsumeContext<AuctionCategorisedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var query = new GetAuctionByIdQuery(context.Message.AuctionId);
        var auction = await sender.Send(query);
        var command = new UpdateAuctionCommand(auction.Auction.Id, auction.Auction.Name, context.Message.Category, auction.Auction.Description, auction.Auction.ImageFile, auction.Auction.EndingDate, auction.Auction.StartingPrice);
        await sender.Send(command);
    }
}
