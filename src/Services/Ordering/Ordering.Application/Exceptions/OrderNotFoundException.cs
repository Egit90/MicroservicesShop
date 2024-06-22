using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public sealed class OrderNotFoundException(Guid id) : NotFoundException("Order", id);