namespace Auction.API.Auctions.GetAuctionByCategory;

public record GetAuctionByCategoryQuery(string Category) : IQuery<GetAuctionByCategoryResult>;

public record GetAuctionByCategoryResult(IEnumerable<Models.Auction> Auctions);

public class GetAuctionByCategoryQueryHandler
    (IAuctionRepository repository)
    : IQueryHandler<GetAuctionByCategoryQuery, GetAuctionByCategoryResult>
{
    public async Task<GetAuctionByCategoryResult> Handle(GetAuctionByCategoryQuery query, CancellationToken cancellationToken)
    {
        var auctions = await repository.GetAuctionByCategory(query.Category, cancellationToken);

        return new GetAuctionByCategoryResult(auctions);
    }
}
