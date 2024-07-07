using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using ErrorOr;

namespace Basket.API.Basket.GetBasket;


public sealed class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, ErrorOr<ShoppingCart>>
{
    public async Task<ErrorOr<ShoppingCart>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // get basket from Marten
        return await repository.GetBasket(request.userName, cancellationToken);
    }
}
