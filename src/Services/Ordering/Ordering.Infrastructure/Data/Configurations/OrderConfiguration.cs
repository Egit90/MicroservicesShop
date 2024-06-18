using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .HasConversion(
                    x => x.Value, // when saving to Db use the Value
                    x => OrderId.Of(x)// When reading from Db create a OrderId type.
                );


        builder.HasOne<Customer>()
                .WithMany() // One Customer has Many orders
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

        builder.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, build =>
            {
                build.Property(n => n.Value)
                     .HasColumnName(nameof(Order.OrderName))
                     .HasMaxLength(100)
                     .IsRequired();
            }
        );

        builder.ComplexProperty(
            o => o.ShippingAddress, build =>
            {
                build.Property(n => n.FirstName)
                     .HasMaxLength(50)
                     .IsRequired();

                build.Property(n => n.LastName)
                     .HasMaxLength(50)
                     .IsRequired();

                build.Property(n => n.EmailAddress)
                     .HasMaxLength(50);

                build.Property(n => n.AddressLine)
                     .HasMaxLength(180)
                     .IsRequired();

                build.Property(n => n.Country)
                     .HasMaxLength(50);

                build.Property(n => n.State)
                     .HasMaxLength(50);

                build.Property(n => n.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();
            }
        );


        builder.ComplexProperty(
            o => o.BillingAddress, build =>
            {
                build.Property(n => n.FirstName)
                     .HasMaxLength(50)
                     .IsRequired();

                build.Property(n => n.LastName)
                     .HasMaxLength(50)
                     .IsRequired();

                build.Property(n => n.EmailAddress)
                     .HasMaxLength(50);

                build.Property(n => n.AddressLine)
                     .HasMaxLength(180)
                     .IsRequired();

                build.Property(n => n.Country)
                     .HasMaxLength(50);

                build.Property(n => n.State)
                     .HasMaxLength(50);

                build.Property(n => n.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();
            }
        );


        builder.ComplexProperty(
            o => o.Payment, build =>
            {
                build.Property(n => n.CardName)
                     .HasMaxLength(50);

                build.Property(n => n.CardNumber)
                     .HasMaxLength(24)
                     .IsRequired();

                build.Property(n => n.Expiration)
                     .HasMaxLength(10);

                build.Property(n => n.CVV)
                     .HasMaxLength(3);

                build.Property(n => n.PaymentMethod)
                     .HasMaxLength(50);
            }
        );

        builder.Property(x => x.Status)
        .HasDefaultValue(OrderStatus.Draft)
        .HasConversion(
            s => s.ToString(),
            dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
        );

        builder.Property(o => o.TotalPrice);
    }
}
