using System.Text.Json;
using Basket.API.Models;
using ErrorOr;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public sealed class CashedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<ErrorOr<ShoppingCart>> GetBasket(string username, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(username, cancellationToken);

        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }
        var basket = await repository.GetBasket(username, cancellationToken);

        if (!basket.IsError)
        {
            await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
        }
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), token: cancellationToken);
        return basket;
    }
    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    {
        var res = await repository.DeleteBasket(username, cancellationToken);
        if (res)
        {
            await cache.RemoveAsync(username, cancellationToken);
        }

        return res;
    }
}