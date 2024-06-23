using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public sealed record GetOrdersByCustomerQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByCustomerQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
                    .Include(x => x.OrderItems)
                    .AsNoTracking()
                    .Where(x => x.CustomerId == CustomerId.Of(request.CustomerId))
                    .OrderBy(x => x.OrderName.Value)
                    .ToListAsync(cancellationToken: cancellationToken);

        return orders.ToOrderDtoList();
    }
}