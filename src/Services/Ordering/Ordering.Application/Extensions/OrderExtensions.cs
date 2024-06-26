using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this List<Order> orders) => orders.Select(DtoFromOrder);

    public static OrderDto ToOrderDto(this Order order) => DtoFromOrder(order);

    private static OrderDto DtoFromOrder(Order order)
    {
        var shippingAddress = new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress ?? "",
                                             order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State,
                                             order.ShippingAddress.ZipCode);

        var BillingAddress = new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress ?? "",
                                            order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State,
                                            order.BillingAddress.ZipCode);

        var payment = new PaymentDto(order.Payment.CardName ?? "", order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod);

        var orderItems = order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList();
        return new(order.Id.Value, order.CustomerId.Value, order.OrderName.Value, shippingAddress, BillingAddress, payment, order.Status, orderItems);
    }
}