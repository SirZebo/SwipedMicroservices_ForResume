using Bidding.Domain.Models;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks.Dataflow;

namespace Bidding.Application.Bids.Commands.CreateBid;


public class UpdateBidCommandHandler
    (IApplicationDbContext dbContext, IBiddingRepository dbBiddingContext)
    : ICommandHandler<UpdateBidCommand, UpdateBidResult>
{
    private static readonly ConcurrentDictionary<AuctionId, SemaphoreSlim> auctionLocks = new();
    private static readonly ConcurrentDictionary<AuctionId, int> auctionUsage = new();

    public async Task<UpdateBidResult> Handle(UpdateBidCommand command, CancellationToken cancellationToken)
    {
        Bid bid;
        var auctionId = AuctionId.Of(command.Bid.AuctionId);

        var auction = await dbContext.Auctions
                .FindAsync([auctionId], cancellationToken: cancellationToken);

        if (auction is null)
        {
            throw new AuctionNotFoundException(command.Bid.AuctionId);
        }

        var auctionLock = auctionLocks.GetOrAdd(auctionId, (_) => new SemaphoreSlim(1, 1));
        var usage = auctionUsage.GetOrAdd(auctionId, 0);
        auctionUsage[auctionId] = usage + 1;
        await auctionLocks[auctionId].WaitAsync(cancellationToken);
        try
        {
            var highestBid = await dbBiddingContext.GetHighestBid(auctionId, cancellationToken);

            if (!(auction.EndingDate > DateTime.UtcNow))
            {
                throw new AuctionAlreadyEndedException(command.Bid.AuctionId);
            }

            if (!(command.Bid.Price > highestBid.Price))
            {
                throw new InvalidBidPriceException(command.Bid.AuctionId, command.Bid.CustomerId, command.Bid.Price);
            }

            bid = CreateNewBid(command.Bid);

            dbContext.Bids.Add(bid);
            await dbBiddingContext.StoreBid(bid, cancellationToken);
        }
        finally
        {
            auctionLocks[auctionId].Release();
        }
        auctionUsage[auctionId] = auctionUsage[auctionId] - 1;

        // If no more bid request for that auction, dispose semaphore 
        if (auctionUsage[auctionId] == 0)
        {
            auctionLocks.TryRemove(auctionId, out _);
            auctionUsage.TryRemove(auctionId, out _);
            auctionLock.Dispose();

        }
        return new UpdateBidResult(true);

        ////This is safer with using lock, but lock does not allow await
        //lock (auctionLock[auctionId])
        //{
        //    var highestbid = dbContext.Bids
        //        .Where(b => auctionId == b.AuctionId)
        //        .Max(b => b.Price);

        //    if (!(auction.EndingDate > DateTime.UtcNow))
        //    {
        //        throw new AuctionAlreadyEndedException(command.Bid.AuctionId);
        //    }

        //    if (!(command.Bid.Price > highestbid))
        //    {
        //        throw new InvalidBidPriceException(command.Bid.AuctionId, command.Bid.CustomerId, command.Bid.Price);
        //    }

        //    bid = CreateNewBid(command.Bid);

        //    dbContext.Bids.Add(bid);
        //    dbContext.SaveChanges();

        //}

        //var auctionLock = auctionLocks.GetOrAdd(auctionId, (_) => new SemaphoreSlim(1, 1));
        //auctionUsage.GetOrAdd(auctionId, 1);
        //await auctionLock.WaitAsync(cancellationToken);

        //auctionLock.Release();
        //auctionUsage[auctionId] = auctionUsage[auctionId] - 1;

        //if (auctionUsage[auctionId] == 0)
        //{
        //    auctionLocks.TryRemove(auctionId, out _);
        //    auctionUsage.TryRemove(auctionId, out _);
        //    auctionLock.Dispose();

        //}


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