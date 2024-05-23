using Basket.API.Models;
using BuildingBlocks.CQRS;
using ErrorOr;

namespace Basket.API.Basket.GetBasket;


public sealed class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, ErrorOr<ShoppingCart>>
{
    public async Task<ErrorOr<ShoppingCart>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        //TODO: get basket from Marten
        //var basket = await _repository.GetBasket(request.UserName);
        await Task.Delay(1000, cancellationToken);
        return new ShoppingCart(" ");
    }
}
