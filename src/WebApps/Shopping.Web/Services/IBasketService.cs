using System.Net;
using Refit;
using Shopping.Web.Models.Basket;

namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<ShoppingCartModel> GetBasket(string userName);

    [Post("/basket-service/basket")]
    Task<string> StoreBasket(StoreBasketRequest Cart);

    [Delete("/basket-service/basket/{userName}")]
    Task<bool> DeleteBasket(string userName);

    [Post("/basket-service/basket/checkout")]
    Task<bool> CheckoutBasket(BasketCheckoutModel request);

    public async Task<ShoppingCartModel> LoadUserBasket()
    {
        var userName = "swn";
        ShoppingCartModel basket;

        try
        {
            basket = await GetBasket(userName);
        }
        catch (ApiException ApiException) when (ApiException.StatusCode == HttpStatusCode.NotFound)
        {
            basket = new ShoppingCartModel
            {
                UserName = userName,
                Items = []
            };
        }

        return basket;
    }


}