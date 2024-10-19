using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidding.Infrastructure.Data.Configurations;
public class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
            orderId => orderId.Value,
            dbId => BidId.Of(dbId)
        );

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasOne<Auction>()
            .WithMany()
            .HasForeignKey(o => o.AuctionId)
            .IsRequired();
    }
}
