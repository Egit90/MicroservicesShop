using System.Net;
using Refit;
using Shopping.Web.Models.Basket;
using Shopping.Web.Pages;

namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket/{userName}")]
    Task<ShoppingCartModel> GetBasket(string userName);

    [Post("/basket")]
    Task<string> StoreBasket(StoreBasketRequest Cart);

    [Delete("/basket/{userName}")]
    Task<bool> DeleteBasket(string userName);

    [Post("/basket/checkout")]
    Task<bool> CheckoutBasket(createOrderRequest Order);

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