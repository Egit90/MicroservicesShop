using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {

        logger.LogInformation("Index Paged");
        ProductList = await catalogService.GetProducts();

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productID)
    {
        logger.LogInformation("Adding Item To Cart");

        var productRes = await catalogService.GetProduct(productID);

        var basket = await basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productID,
            ProductName = productRes.Name,
            Price = productRes.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketService.StoreBasket(basket);

        return Redirect("Cart");
    }


}