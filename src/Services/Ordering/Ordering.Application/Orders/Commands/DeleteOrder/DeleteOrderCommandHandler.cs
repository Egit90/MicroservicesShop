using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Orders.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.OrderId);
        var order = await context.Orders.FindAsync(orderId, cancellationToken)
                    ?? throw new OrderNotFoundException(request.OrderId);

        context.Orders.Remove(order);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}