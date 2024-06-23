using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<PaginationResult<OrderDto>>;