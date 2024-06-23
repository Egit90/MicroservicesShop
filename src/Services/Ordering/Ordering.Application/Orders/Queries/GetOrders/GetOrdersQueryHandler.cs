using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public sealed class GetOrdersQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, PaginationResult<OrderDto>>
{
    public async Task<PaginationResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var pageIndex = request.PaginationRequest.PageIndex;
        var pageSize = request.PaginationRequest.PageSize;

        var totalCount = await context.Orders.LongCountAsync(cancellationToken);

        var orders = await context.Orders
                           .Include(x => x.OrderItems)
                           .OrderBy(x => x.OrderName.Value)
                           .Skip(pageSize * pageIndex)
                           .Take(pageSize)
                           .ToListAsync(cancellationToken);

        return new PaginationResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList());
    }
}