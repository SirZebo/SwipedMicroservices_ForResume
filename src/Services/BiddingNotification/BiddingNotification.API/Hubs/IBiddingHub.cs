using BiddingNotification.API.Models;

namespace BiddingNotification.API.Hubs;

public interface IBiddingHub
{
    public  Task GetBid();
    public  Task SendSseByAuctionId(Bid bid);

}
