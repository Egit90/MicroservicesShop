using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .HasConversion(
                    x => x.Value, // when saving to Db use the Value
                    x => ProductId.Of(x)// When reading from Db create a ProductId type.
                );

        builder.Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired();
    }
}
