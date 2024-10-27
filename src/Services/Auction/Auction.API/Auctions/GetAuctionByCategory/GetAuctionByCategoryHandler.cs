namespace Auction.API.Auctions.GetAuctionByCategory;

public record GetAuctionByCategoryQuery(string Category) : IQuery<GetAuctionByCategoryResult>;

public record GetAuctionByCategoryResult(IEnumerable<Models.Auction> Auctions);

public class GetAuctionByCategoryQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetAuctionByCategoryQuery, GetAuctionByCategoryResult>
{
    public async Task<GetAuctionByCategoryResult> Handle(GetAuctionByCategoryQuery query, CancellationToken cancellationToken)
    {
        var auctions = await session.Query<Models.Auction>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync();

        return new GetAuctionByCategoryResult(auctions);
    }
}
