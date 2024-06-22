using BuildingBlocks.Exceptions;

namespace Ordering.Application.Orders.Exceptions;

public sealed class OrderNotFoundException(Guid id) : NotFoundException("Order", id);