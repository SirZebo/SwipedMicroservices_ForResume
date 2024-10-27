using Bidding.Application.Bids.Commands.CreateBid;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Bidding.Application.Bids.EventHandlers.Integration;
public class AuctionCreatedEventHandler
    (ISender sender,
    IApplicationDbContext dbContext,
    ILogger<AuctionCreatedEventHandler> logger)
    : IConsumer<AuctionCreatedEvent>
{
    public async Task Consume(ConsumeContext<AuctionCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var auction = MapToAuction(context.Message);
        dbContext.Auctions.Add(auction);
        await dbContext.SaveChangesAsync(context.CancellationToken);
        
        var command = MapToCreateBidCommand(context.Message);
        CreateBidResult createBidResult = await sender.Send(command);
    }

    private CreateBidCommand MapToCreateBidCommand(AuctionCreatedEvent message)
    {
        var bidDto = new BidDto
        (
            Id: new Guid(),
            AuctionId: message.AuctionId,
            CustomerId: new Guid(),
            Price: 0
        );
        return new CreateBidCommand(bidDto);
    }

    private Auction MapToAuction(AuctionCreatedEvent message)
    {
        var auction = Auction.Create(AuctionId.Of(message.AuctionId), message.Name, message.StartingPrice, message.EndingDate);
        return auction;
    }

    
}
