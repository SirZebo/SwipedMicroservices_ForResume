using Bidding.Domain.Abstractions;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Bidding.Application.Bids.EventHandlers.Integration;
public class AuctionEndedEventHandler
    (
    ILogger<AuctionEndedEventHandler> logger)
    : IConsumer<AuctionEndedEvent>
{
    public async Task Consume(ConsumeContext<AuctionEndedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        await Task.CompletedTask;
    }
}
