using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(OrderDto Order) : ICommand<bool>;
