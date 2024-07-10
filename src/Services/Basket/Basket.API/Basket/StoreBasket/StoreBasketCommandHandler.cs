using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Catalog.API;
using Discount.Grpcs;

namespace Basket.API.Basket.StoreBasket;

public sealed class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto, PriceProtoService.PriceProtoServiceClient PricesProto) : ICommandHandler<StoreBasketCommand, string>
{
    public async Task<string> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        // Verify the Cart Prices
        var updatedCart = await VerifyPrices(request, cancellationToken: cancellationToken);

        //  call Discount grpc
        await DeductDiscount(updatedCart, cancellationToken);

        // store Basket in db
        var res = await repository.StoreBasket(updatedCart, cancellationToken);

        // update cache
        return request.Cart.UserName;
    }



    private async Task<ShoppingCart> VerifyPrices(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        // Get Original Prices
        var productIdList = request.Cart.Items.Select(x => x.ProductId.ToString()).ToList();
        var getPricesRequest = new GetPricesRequest();
        getPricesRequest.ProductIdList.AddRange(productIdList);

        var PricesList = await PricesProto.GetPricesAsync(getPricesRequest, cancellationToken: cancellationToken);

        // Update prices in ShoppingCartItems
        var updatedItems = request.Cart.Items.Select(item =>
        {
            var priceInfo = PricesList.Prices.FirstOrDefault(p => p.Id == item.ProductId.ToString());
            if (priceInfo != null)
            {
                // Create a new ShoppingCartItem with updated Price
                return new ShoppingCartItem
                {
                    Quantity = item.Quantity,
                    Color = item.Color,
                    Price = (decimal)priceInfo.Price * item.Quantity,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                };
            }
            else
            {
                // If price info not found, return the original item
                return item;
            }
        }).ToList();

        // Create a new ShoppingCart with updated items
        var updatedCart = new ShoppingCart
        {
            UserName = request.Cart.UserName,
            Items = updatedItems
        };

        return updatedCart;
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
