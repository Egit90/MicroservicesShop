using Basket.API.Data;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public sealed class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, bool>
{
    public async Task<bool> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(request.BasketCheckOutDto.UserName, cancellationToken);

        if (basket.IsError || basket.Value == null) return false;

        var eventMessage = request.BasketCheckOutDto.Adapt<BasketCheckoutEvent>();

        eventMessage.TotalPrice = basket.Value.TotalPrice;
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await repository.DeleteBasket(request.BasketCheckOutDto.UserName, cancellationToken);

        return true;
    }
}