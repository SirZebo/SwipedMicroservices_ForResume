using BiddingNotification.API.Models;
using BuildingBlocks.CQRS;

namespace BiddingNotification.API.BiddingNotifications.GetBidById;

public record GetBidByIdQuery(Guid Id) : IQuery<GetBidByIdResult>;

public record GetBidByIdResult(Bid Bid);
internal class GetBidByIdQueryHandler
    : IQueryHandler<GetBidByIdQuery, GetBidByIdResult>
{
    public async Task<GetBidByIdResult> Handle(GetBidByIdQuery query, CancellationToken cancellationToken)
    {
        //var bid = await repository.GetAuctionById(query.Id, cancellationToken);
        var bid = new Bid
        {
            BidId = new Guid("0192d9c7-27de-43eb-a290-f9cab61e51fc"),
            AuctionId = new Guid("6EC1297B-EC0A-4AA1-BE25-6726E3B51A27"),
            CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
            Price = 700
        };

        if (bid is null)
        {
            //throw new BidNotFoundException(query.Id);
        }
        await Task.CompletedTask;
        return new GetBidByIdResult(bid);
    }
}