namespace BiddingResult.API.BiddingResults.GetContracts;

public record GetContractQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetContractResult>;

public record GetContractResult(IEnumerable<Contract> Contracts);

internal class GetContractsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetContractQuery, GetContractResult>
{
    public async Task<GetContractResult> Handle(GetContractQuery query, CancellationToken cancellationToken)
    {
        var contracts = await session.Query<Contract>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetContractResult(contracts);
    }
}
