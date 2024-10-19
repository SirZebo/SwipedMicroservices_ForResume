using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidding.Infrastructure.Data.Configurations;
public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            productId => productId.Value,
            dbId => AuctionId.Of(dbId)
            );

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
    }
}