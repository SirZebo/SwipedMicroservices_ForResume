using BiddingNotification.API.BiddingNotifications.GetBidById;
using BiddingNotification.API.Data;
using BiddingNotification.API.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace BiddingNotification.API.Hubs;

public class BiddingHub
    (ISender sender, ILogger<BiddingHub> logger)
    : Hub
{

    public override async Task OnConnectedAsync()
  {
        string id = Context?.GetHttpContext()?.GetRouteValue("auctionId") as string;

        var connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, id);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string id = Context?.GetHttpContext()?.GetRouteValue("auctionId") as string;

        var connectionId = Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(connectionId, id);

        await base.OnDisconnectedAsync(exception);
    }

    public async Task GetBid()
    {
        string id = Context?.GetHttpContext()?.GetRouteValue("auctionId") as string;

        var result = await sender.Send(new GetBidByIdQuery(new Guid(id)));

        await Clients.Caller
            .SendAsync("GetBid", result.Bid);
    }

    public async Task SendSseByAuctionId(Bid bid)
    {
            await Clients.Groups(bid.AuctionId.ToString())
                .SendAsync("SendSseByAuctionId", bid);
    }
}

