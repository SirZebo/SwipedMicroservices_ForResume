using Bidding.Domain.Models;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks.Dataflow;

namespace Bidding.Application.Bids.Commands.CreateBid;


public class CreateBidCommandHandler
    (IApplicationDbContext dbContext, IBiddingRepository dbBiddingContext)
    : ICommandHandler<CreateBidCommand, CreateBidResult>
{

    public async Task<CreateBidResult> Handle(CreateBidCommand command, CancellationToken cancellationToken)
    {
        var bid = CreateNewBid(command.Bid);

        dbContext.Bids.Add(bid);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBidResult(bid.Id.Value);
    }
    private Bid CreateNewBid(BidDto bidDto)
    {
        var newBid = Bid.Create(
            id: BidId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(bidDto.CustomerId),
            auctionId: AuctionId.Of(bidDto.AuctionId),
            price: bidDto.Price);

        return newBid;

    }
}