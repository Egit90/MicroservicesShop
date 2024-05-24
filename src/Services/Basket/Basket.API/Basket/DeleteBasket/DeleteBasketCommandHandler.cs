using System.Data;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.DeleteBasket;

public sealed class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, bool>
{
    public Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // Delete Basket from DB
        // Delete Basket from Cache

        return Task.FromResult(true);
    }
}

