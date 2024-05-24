using System.Data;
using Basket.API.Data;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.DeleteBasket;

public sealed class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, bool>
{
    public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // Delete Basket from DB
        var d = await repository.DeleteBasket(request.UserName, cancellationToken);
        // Delete Basket from Cache

        return d;
    }
}

