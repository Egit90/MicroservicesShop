using Basket.API.Models;
using BuildingBlocks.CQRS;
using ErrorOr;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string userName) : IQuery<ErrorOr<ShoppingCart>>;