using BiddingNotification.API.Models;

namespace BiddingNotification.API.Hubs;

public interface IBiddingClient
{
    Task ReceiveBid(Bid bid);
    Task SendSseByAuctionId(Bid bid);
}
