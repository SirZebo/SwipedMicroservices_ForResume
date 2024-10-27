
using Auction.API.Exceptions;

namespace Auction.API.Auctions.GetAuctionById;

public record GetAuctionByIdQuery(Guid Id) : IQuery<GetAuctionByIdResult>;

public record GetAuctionByIdResult(Models.Auction Auction);
internal class GetAuctionByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetAuctionByIdQuery, GetAuctionByIdResult>
{
    public async Task<GetAuctionByIdResult> Handle(GetAuctionByIdQuery query, CancellationToken cancellationToken)
    {
        var auction = await session.LoadAsync<Models.Auction>(query.Id, cancellationToken);

        if (auction is null)
        {
            throw new AuctionNotFoundException(query.Id);
        }

        return new GetAuctionByIdResult(auction);
    }
}
