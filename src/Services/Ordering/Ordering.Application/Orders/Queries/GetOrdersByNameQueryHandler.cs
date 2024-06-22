using BuildingBlocks.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries;

public sealed class GetOrdersByNameQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
                    .Include(x => x.OrderItems)
                    .AsNoTracking()
                    .Where(x => x.OrderName.Value.Contains(request.orderName))
                    .OrderBy(x => x.OrderName)
                    .ToListAsync(cancellationToken: cancellationToken);


        var orderDtos = ProjectToOrderDto(orders);

        return orderDtos;
    }

    private IEnumerable<OrderDto> ProjectToOrderDto(List<Order> orders)
    {
        List<OrderDto> results = [];

        orders.ForEach(order =>
        {
            var orderDto = new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    EmailAddress: order.ShippingAddress.EmailAddress ?? "",
                    AddressLine: order.ShippingAddress.AddressLine,
                    Country: order.ShippingAddress.Country,
                    State: order.ShippingAddress.State,
                    ZipCode: order.ShippingAddress.ZipCode
                ),
                BillingAddress: new AddressDto(
                    FirstName: order.BillingAddress.FirstName,
                    LastName: order.BillingAddress.LastName,
                    EmailAddress: order.BillingAddress.EmailAddress ?? "",
                    AddressLine: order.BillingAddress.AddressLine,
                    Country: order.BillingAddress.Country,
                    State: order.BillingAddress.State,
                    ZipCode: order.BillingAddress.ZipCode
                ),
                Payment: new PaymentDto(
                    CardName: order.Payment.CardName ?? "",
                    CardNumber: order.Payment.CardNumber,
                    Expiration: order.Payment.Expiration,
                    Cvv: order.Payment.CVV,
                    PaymentMethod: order.Payment.PaymentMethod
                ),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList()
                );

            results.Add(orderDto);
        });

        return results;
    }
}
