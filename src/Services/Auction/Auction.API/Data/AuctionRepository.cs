
using Auction.API.Exceptions;

namespace Auction.API.Data;

public class AuctionRepository
    (IDocumentSession session)
    : IAuctionRepository
{
    public async Task<Models.Auction> GetAuctionById(Guid id, CancellationToken cancellationToken = default)
    {
        var auction = await session.LoadAsync<Models.Auction>(id, cancellationToken);

        return auction is null ? throw new AuctionNotFoundException(id) : auction;
    }

    public async Task<IEnumerable<Models.Auction>> GetAuctionByCategory(string category, CancellationToken cancellationToken = default)
    {
        var auctions = await session.Query<Models.Auction>()
            .Where(p => p.Category.Contains(category))
            .ToListAsync();

        return auctions;
    }

    public async Task<IEnumerable<Models.Auction>> GetAuctions(CancellationToken cancellationToken = default)
    {
        var auctions = await session.Query<Models.Auction>()
            .ToListAsync(cancellationToken);

        return auctions;
    }

    public async Task<Models.Auction> StoreAuction(Models.Auction auction, CancellationToken cancellationToken = default)
    {
        session.Store(auction);
        await session.SaveChangesAsync(cancellationToken);
        return auction;
    }

    public async Task<Models.Auction> UpdateAuction(Models.Auction newAuction, CancellationToken cancellationToken = default)
    {
        var auction = await session.LoadAsync<Models.Auction>(newAuction.Id, cancellationToken);
        var oldAuction = auction.Adapt<Models.Auction>();

        if (auction is null)
        {
            throw new AuctionNotFoundException(newAuction.Id);
        }

        auction.Name = newAuction.Name;
        auction.Category = newAuction.Category;
        auction.Description = newAuction.Description;
        auction.ImageFile = newAuction.ImageFile;
        auction.EndingDate = newAuction.EndingDate;
        auction.StartingPrice = newAuction.StartingPrice;

        session.Update(auction);
        await session.SaveChangesAsync(cancellationToken);

        return oldAuction;
    }
}
