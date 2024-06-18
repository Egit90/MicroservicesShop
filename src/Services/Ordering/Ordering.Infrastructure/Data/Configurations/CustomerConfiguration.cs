using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .HasConversion(
                    customerId => customerId.Value, // when saving to Db use the Value
                    dbId => CustomerId.Of(dbId)     // When reading from Db create a CustomerId type.
                );

        builder.Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Email)
                .HasMaxLength(250);

        builder.HasIndex(c => c.Email)
               .IsUnique();
    }
}
