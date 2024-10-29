using Marten.Schema;

namespace BiddingResult.API.Data;

public class ContractInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Contract>().AnyAsync())
            return;

        session.Store<Contract>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Contract> GetPreconfiguredProducts() => new List<Contract>()
            {
                new Contract()
                {
                    AuctionId = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                    CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                    Price = 950.00M,
                    Status = PaymentStatus.NotPaid
                },
                
            };
}