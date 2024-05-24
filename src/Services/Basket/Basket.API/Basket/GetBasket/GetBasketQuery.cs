using Basket.API.Models;
using BuildingBlocks.CQRS;
using ErrorOr;

namespace Basket.API.Basket.GetBasket;

public sealed record GetBasketQuery(string userName) : IQuery<ErrorOr<ShoppingCart>>;