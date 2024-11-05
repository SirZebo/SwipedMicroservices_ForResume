using Marten.Schema;
using Marten;
using BiddingNotification.API.Models;

namespace BiddingNotification.API.Data;

public class BidInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Bid>().AnyAsync())
            return;

        session.Store<Bid>(GetPreconfiguredAuctions());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Bid> GetPreconfiguredAuctions() => new List<Bid>()
            {
                new Bid
            {
                Id = Guid.NewGuid(),
                AuctionId= new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                CustomerId= new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                Price = 500
            },
            new Bid
            {
                Id = Guid.NewGuid(),
                AuctionId= new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                CustomerId= new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                Price = 400
            },
            new Bid
            {
                Id = Guid.NewGuid(),
                AuctionId= new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
                CustomerId= new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                Price = 650
            },
            new Bid
            {
                Id = Guid.NewGuid(),
                AuctionId= new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
                CustomerId= new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                Price = 450
            },
    };
}
