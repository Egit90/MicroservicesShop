using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Extensions;

public static class ShoppingCartModelExtensions
{
    public static async Task<bool> AddToCart(this ShoppingCartModel basket, Guid productId, ICatalogService catalogService, int quantity = 1)
    {
        try
        {
            var productRes = await catalogService.GetProduct(productId);
            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);
            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                basket.Items.Add(new()
                {
                    ProductId = productId,
                    ProductName = productRes.Name,
                    Price = productRes.Price,
                    Quantity = quantity,
                    Color = "Black"
                });
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}