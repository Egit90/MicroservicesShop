using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(Guid OrderId) : ICommand<bool>;
