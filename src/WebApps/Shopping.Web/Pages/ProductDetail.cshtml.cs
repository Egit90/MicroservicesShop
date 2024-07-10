using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Extensions;
using Shopping.Web.Models.Basket;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class ProductDetailModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductDetailModel> logger)
            : PageModel
{
    public ProductModel Product { get; set; } = default!;

    [BindProperty] public string Color { get; set; } = default!;

    [BindProperty] public string Quantity { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid productId)
    {
        Product = await catalogService.GetProduct(productId);
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId, int quantity)
    {
        logger.LogInformation("Adding To Cart");

        var basket = await basketService.LoadUserBasket();

        var isAdded = await basket.AddToCart(productId, catalogService, quantity);

        var request = new StoreBasketRequest(basket);

        await basketService.StoreBasket(request);

        return RedirectToPage("Cart");
    }
}


