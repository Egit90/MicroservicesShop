using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Extensions;
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
        logger.LogInformation("Adding To Cart");

        var basket = await basketService.LoadUserBasket();

        var isAdded = await basket.AddToCart(productID, catalogService);

        var request = new StoreBasketRequest(basket);

        await basketService.StoreBasket(request);

        return RedirectToPage("Cart");
    }


}