using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Discount.Grpcs;

namespace Basket.API.Basket.StoreBasket;

public sealed class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, string>
{
    public async Task<string> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {

        //  call Discount grpc
        await DeductDiscount(request.Cart, cancellationToken);

        // store Basket in db
        var res = await repository.StoreBasket(request.Cart, cancellationToken);

        // update cache
        return request.Cart.UserName;
    }


    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }

    }


}
