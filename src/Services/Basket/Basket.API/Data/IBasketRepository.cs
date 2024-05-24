using Basket.API.Models;
using ErrorOr;

namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ErrorOr<ShoppingCart>> GetBasket(string username, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default);
}