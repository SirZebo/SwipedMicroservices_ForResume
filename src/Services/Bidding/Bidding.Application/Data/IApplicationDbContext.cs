namespace Bidding.Application.Data;
public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Auction> Auctions { get; }
    DbSet<Bid> Bids { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}
