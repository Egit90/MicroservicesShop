using BuildingBlocks.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public sealed class GetOrdersByNameQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
                    .Include(x => x.OrderItems)
                    .AsNoTracking()
                    .Where(x => x.OrderName.Value.Contains(request.orderName))
                    .OrderBy(x => x.OrderName.Value)
                    .ToListAsync(cancellationToken: cancellationToken)
                    ?? throw new OrderNotFoundException(Guid.Empty);

        return orders.ToOrderDtoList();
    }
}
