using BiddingNotification.API.Data;
using BiddingNotification.API.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace BiddingNotification.API.BiddingNotifications.GetBidById;

public record GetBidByIdQuery(Guid Id) : IQuery<GetBidByIdResult>;

public record GetBidByIdResult(Bid Bid);
public class GetBidByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetBidByIdQuery, GetBidByIdResult>
{
    public async Task<GetBidByIdResult> Handle(GetBidByIdQuery query, CancellationToken cancellationToken)
    {
        //var bid = await session.LoadAsync<Bid>(query.Id, cancellationToken);



        //var bid = await repository.GetAuctionById(query.Id, cancellationToken);
        var bid = new Bid
        {
            Id = new Guid("0192d9c7-27de-43eb-a290-f9cab61e51fc"),
            AuctionId = new Guid("6EC1297B-EC0A-4AA1-BE25-6726E3B51A27"),
            CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
            Price = 700
        };

        return bid is null ? throw new Exception() : new GetBidByIdResult(bid);
    }
}