using Basket.API.Data;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.StoreBasket;

public sealed class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, string>
{
    public async Task<string> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.Cart;

        // store Basket in db
        var res = await repository.StoreBasket(cart, cancellationToken);

        // update cache
        return request.Cart.UserName;
    }
}
