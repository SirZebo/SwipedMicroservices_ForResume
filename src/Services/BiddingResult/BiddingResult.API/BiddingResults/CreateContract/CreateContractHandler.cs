namespace BiddingResult.API.BiddingResults.CreateContract;

public record CreateContractCommand(Guid AuctionId, Guid CustomerId, decimal Price, PaymentStatus Status)
    : ICommand<CreateContractResult>;

public record CreateContractResult(Guid Id);

public class CreateContractCommandHandler
    (IDocumentSession session)
    : ICommandHandler<CreateContractCommand, CreateContractResult>
{
    public async Task<CreateContractResult> Handle(CreateContractCommand command, CancellationToken cancellationToken)
    {
        var contract = new Contract
        {
            AuctionId = command.AuctionId,
            CustomerId = command.CustomerId,
            Price = command.Price,
            Status = command.Status
        };

        // save to database
        session.Store(contract);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new CreateContractResult(contract.Id);
    }
}
