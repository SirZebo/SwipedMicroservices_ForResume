namespace Auction.API.Auctions.GetAuctions;

public record GetAuctionsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetAuctionsResult>;

public record GetAuctionsResult(IEnumerable<Models.Auction> Auctions);

internal class GetAuctionsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetAuctionsQuery, GetAuctionsResult>
{
    public async Task<GetAuctionsResult> Handle(GetAuctionsQuery query, CancellationToken cancellationToken)
    {
        var auctions = await session.Query<Models.Auction>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetAuctionsResult(auctions);
    }
}
