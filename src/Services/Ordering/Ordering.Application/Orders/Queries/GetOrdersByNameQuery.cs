using BuildingBlocks.CQRS;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries;

public sealed record GetOrdersByNameQuery(string orderName) : IQuery<IEnumerable<OrderDto>>;
