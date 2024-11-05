using BiddingNotification.API.BiddingNotifications.GetBidById;
using BiddingNotification.API.Data;
using BiddingNotification.API.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace BiddingNotification.API.Hubs;

public class BiddingHub : Hub<IBiddingClient>, IBiddingHub
{
    private readonly IHubContext<BiddingHub> _context;
    private readonly ISender _sender;
    private readonly ILogger<BiddingHub> _logger;

    public BiddingHub(IHubContext<BiddingHub> context, ISender sender, ILogger<BiddingHub> logger)
    {

        _sender = sender;
        _logger = logger;
        _context = context;
    }


    public override async Task OnConnectedAsync()
  {
        string id = Context?.GetHttpContext()?.GetRouteValue("auctionId") as string;

        var connectionId = Context.ConnectionId;
        await _context.Groups.AddToGroupAsync(connectionId, id);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string id = Context?.GetHttpContext()?.GetRouteValue("auctionId") as string;

        var connectionId = Context.ConnectionId;
        await _context.Groups.RemoveFromGroupAsync(connectionId, id);

        await base.OnDisconnectedAsync(exception);
    }

    public async Task GetBid()
    {
        string id = Context?.GetHttpContext()?.GetRouteValue("auctionId") as string;

        var result = await _sender.Send(new GetBidByIdQuery(new Guid(id)));

        await Clients.Caller.ReceiveBid(result.Bid);
    }

    public async Task SendSseByAuctionId(Bid bid)
    {
        await _context.Clients.All.SendAsync("SendSseByAuctionId", bid);
    }
    //public async Task GetBid()
    //{
    //    string id = Context?.GetHttpContext()?.GetRouteValue("auctionId") as string;

    //    var result = await _sender.Send(new GetBidByIdQuery(new Guid(id)));

    //    await Clients.Caller.GetBid( result.Bid);
    //}

    //public Task SendSseByAuctionId(Bid bid)
    //{
    //        return this._context.Clients.Groups(bid.AuctionId.ToString())
    //            .SendAsync("SendSseByAuctionId", bid);
    //}
}

