using BuildingBlocks.CQRS;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(Guid OrderId) : ICommand<bool>;
