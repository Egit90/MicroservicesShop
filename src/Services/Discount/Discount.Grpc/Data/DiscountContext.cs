using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public sealed class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)

{
    public DbSet<Coupon> Coupons { get; set; }
}