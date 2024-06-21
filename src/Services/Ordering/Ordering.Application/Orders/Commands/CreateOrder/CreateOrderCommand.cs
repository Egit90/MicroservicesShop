using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderResult(Guid Id);
public sealed record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
