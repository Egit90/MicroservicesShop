using Basket.API.Dtos;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.CheckoutBasket;

public sealed record CheckoutBasketCommand(BasketCheckOutDto BasketCheckOutDto) : ICommand<bool>;
