using BiddingResult.API.BiddingResults.CreateContract;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BiddingResult.API.BiddingResults.EventHandler.Integration;

public class BidEndedEventHandler
    (ISender sender, ILogger<BidEndedEventHandler> logger)
    : IConsumer<BidEndedEvent>
{
    public async Task Consume(ConsumeContext<BidEndedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateContractCommand(context.Message);
        await sender.Send(command);
    }

    private CreateContractCommand MapToCreateContractCommand(BidEndedEvent message)
    {
        var command = new CreateContractCommand(
            AuctionId: message.AuctionId,
            CustomerId: message.CustomerId,
            Price: message.Price,
            Status: PaymentStatus.NotPaid
            );

        return command;
    }
}
