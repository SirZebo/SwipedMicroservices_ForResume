using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidding.Infrastructure.Data.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Value, // Converting Customer Id for db
            dbId => CustomerId.Of(dbId) // Converting CustomerId from db to value obj
            );

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

        builder.Property(c => c.Email).HasMaxLength(255).IsRequired();

        builder.HasIndex(c => c.Email).IsUnique();
    }
}
