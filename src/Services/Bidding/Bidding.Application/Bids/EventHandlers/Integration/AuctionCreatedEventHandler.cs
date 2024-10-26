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
        logger.LogInformation("Integration Event handled: {Id}", context.Message.Id);
        var auction = MapToAuction(context.Message);
        dbContext.Auctions.Add(auction);
        dbContext.SaveChanges();
        
        var command = MapToCreateBidCommand(context.Message.Id);
        await sender.Send(command);
    }

    private CreateBidCommand MapToCreateBidCommand(Guid auctionId)
    {
        var bidDto = new BidDto
        (
            Id: new Guid(),
            AuctionId: auctionId,
            CustomerId: new Guid(),
            Price: 0
        );
        return new CreateBidCommand(bidDto);
    }

    private Auction MapToAuction(AuctionCreatedEvent message)
    {
        var auction = Auction.Create(AuctionId.Of(message.Id), message.Name, message.StartingPrice, message.EndingDate);
        return auction;
    }

    
}
