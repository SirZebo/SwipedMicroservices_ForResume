using Marten.Schema;

namespace Auction.API.Data;

public class AuctionInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Models.Auction>().AnyAsync())
            return;

        session.Store<Models.Auction>(GetPreconfiguredAuctions());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Models.Auction> GetPreconfiguredAuctions() => new List<Models.Auction>()
            {
                new Models.Auction()
                {
                    Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                    Name = "IPhone X",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "Auction-1.png",
                    StartingPrice = 950.00M,
                    EndingDate = DateTime.Now.AddDays(3),
                    Category = new List<string> { "Smart Phone" }
                },
            };
}

