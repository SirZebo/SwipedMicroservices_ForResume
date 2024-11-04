using BiddingNotification.API.BiddingNotifications.GetBidById;
using BiddingNotification.API.Data;
using BiddingNotification.API.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace BiddingNotification.API.Hubs;

public class BiddingHub
    (ISender sender)
    : Hub
{

    public override async Task OnConnectedAsync()
    {
        string id = Context.GetHttpContext().Request.Query["id"];
        id = "58c49479-ec65-4de2-86e7-033c546291aa";

        var connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, id);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string id = Context.GetHttpContext().Request.Query["id"];

        var connectionId = Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(connectionId, id);

        await base.OnDisconnectedAsync(exception);
    }

    public async Task GetBid()
    {
        string id = Context.GetHttpContext().Request.Query["id"];

        var result = await sender.Send(new GetBidByIdQuery(new Guid(id)));

        await Clients.Caller
            .SendAsync("RecieveBiddingInformation", result.Bid);
    }

    public async Task SendSseByAuctionId(Bid bid)
    {
            await Clients.Groups(bid.AuctionId.ToString())
                .SendAsync("ReceiveSpecificMessage", bid);
    }
}

