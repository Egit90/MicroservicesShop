using BuildingBlocks.CQRS;

namespace Basket.API.Basket.StoreBasket;

public sealed class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, string>
{
    public Task<string> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.Cart;

        // store Basket in db
        // update cache

        return Task.FromResult("test");
    }
}
