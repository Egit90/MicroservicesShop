using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Discount.Grpcs;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public sealed class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
                            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
                            ?? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };

        logger.LogInformation("Discount is retrieved for product: {productName} , Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = request.Coupon.Adapt<Coupon>()
                        ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is created for product: {productName} , Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = request.Coupon.Adapt<Coupon>()
                        ?? throw new RpcException(new Status(StatusCode.NotFound, "Product was not found"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is updated for product: {productName} , Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
                       .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
                       ?? throw new RpcException(new Status(StatusCode.NotFound, "Product was not found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is deleted for product: {productName} , Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return new DeleteDiscountResponse() { Success = "true" };
    }
}