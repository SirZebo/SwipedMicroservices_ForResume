namespace BiddingResult.API.BiddingResults.GetContractById;
public record GetContractByIdQuery(Guid Id) : IQuery<GetContractByIdResult>;

public record GetContractByIdResult(Contract Contract);
public class GetContractByIdHandler
    (IDocumentSession session)
    : IQueryHandler<GetContractByIdQuery, GetContractByIdResult>
{
    public async Task<GetContractByIdResult> Handle(GetContractByIdQuery query, CancellationToken cancellationToken)
    {
        var contract = await session.LoadAsync<Contract>(query.Id, cancellationToken);

        if (contract is null)
        {
            throw new ContractNotFoundException(query.Id);
        }

        return new GetContractByIdResult(contract);
    }
}
