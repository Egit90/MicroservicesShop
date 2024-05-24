using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketCommand(ShoppingCart Cart) : ICommand<string>;
