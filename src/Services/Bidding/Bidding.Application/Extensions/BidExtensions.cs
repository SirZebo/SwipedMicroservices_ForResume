using MassTransit.Transports;

namespace Bidding.Application.Extensions;
public static class BidExtensions
{
    public static BidDto ToBidDto(this Bid bid)
    {
        return DtoFromBid(bid);
    }

    private static BidDto DtoFromBid(Bid bid)
    {
        return new BidDto(
                    Id: bid.Id.Value,
                    CustomerId: bid.CustomerId.Value,
                    AuctionId: bid.AuctionId.Value,
                    Price: bid.Price
                );
    }
}
