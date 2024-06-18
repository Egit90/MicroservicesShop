using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .HasConversion(
                    x => x.Value, // when saving to Db use the Value
                    x => OrderItemId.Of(x)// When reading from Db create a OrderItemId type.
                );

        builder.HasOne<Product>() //One Product
                .WithMany()       // Has Many OrderItems
                .HasForeignKey(x => x.ProductId); // with fk of productId

        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Price).IsRequired();

    }
}
