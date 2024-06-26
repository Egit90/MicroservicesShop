using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckOutEventHandler(ISender sender, ILogger<BasketCheckOutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handler: {Integration Event}", context.Message.GetType().Name);

        var cmd = MapToCreateOrderCommand(context.Message);
        await sender.Send(cmd);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(orderId, message.CustomerId, message.UserName, addressDto, addressDto, paymentDto, Ordering.Domain.Enums.OrderStatus.Pending,
        [
            new OrderItemDto(orderId,new Guid("65bf5365-de92-9220-58b7-0478d52198ab") , 2 ,500 ),
            new OrderItemDto(orderId,new Guid("2da61073-27f4-a095-bedc-7100ad1ba37e") , 1 , 400)
        ]
         );

        return new CreateOrderCommand(orderDto);
    }
}