﻿using BuildingBlocks.Messaging.Events;
using Hangfire;
using JobScheduler.API.Jobs.AuctionEnded;
using MassTransit;
using MediatR;

namespace JobScheduler.API.Jobs.EventHandler.Integration;

public class AuctionCreatedEventHandler
    (ISender sender,
    IBackgroundJobClient jobClient,
    IMessageScheduler scheduler,
    IPublishEndpoint publishEndpoint,
    ILogger<AuctionCreatedEventHandler> logger)
    : IConsumer<AuctionCreatedEvent>
{
    public async Task Consume(ConsumeContext<AuctionCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var auctionEndedEvent = new AuctionEndedEvent { AuctionId = context.Message.AuctionId };
        await scheduler.SchedulePublish(DateTime.UtcNow.AddSeconds(20), auctionEndedEvent);
        await Task.CompletedTask;
        //jobClient.Schedule(() => ScheduleJob(auctionEndedEvent), TimeSpan.FromSeconds(5)); //context.Message.EndingDate.TimeOfDay
        //await Task.CompletedTask;

        //IRequest<AuctionEndedResult> command = new AuctionEndedCommand(context.Message.AuctionId);
        //var timespan = context.Message.EndingDate.TimeOfDay;
        //commandsScheduler.Schedule(command, timespan);
    }

    public void ScheduleJob(AuctionEndedEvent auctionEndedEvent)
    {
        publishEndpoint.Publish(auctionEndedEvent);
    }
}