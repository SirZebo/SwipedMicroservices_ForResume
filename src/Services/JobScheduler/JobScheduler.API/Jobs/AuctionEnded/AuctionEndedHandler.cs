using BuildingBlocks.CQRS;
using MediatR;

namespace JobScheduler.API.Jobs.AuctionEnded;


public record AuctionEndedCommand(Guid AuctionId)
    : ICommand<AuctionEndedResult>;

public record AuctionEndedResult(bool IsSuccess);
public class AuctionEndedHandler : ICommandHandler<AuctionEndedCommand, AuctionEndedResult>
{
    public Task<AuctionEndedResult> Handle(AuctionEndedCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
