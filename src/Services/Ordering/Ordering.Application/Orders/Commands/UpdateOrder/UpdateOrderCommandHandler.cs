using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateOrderCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.Order.Id);
        var order = await context.Orders
                    .Include(x => x.OrderItems)
                    .Where(x => x.Id == orderId)
                    .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new OrderNotFoundException(request.Order.Id);

        UpdateOrderWithNewValues(order, request.Order);
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }


    private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var UpdatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var UpdatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        var UpdatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);


        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: UpdatedShippingAddress,
            billingAddress: UpdatedBillingAddress,
            payment: UpdatedPayment,
            status: orderDto.Status
        );

    }
}