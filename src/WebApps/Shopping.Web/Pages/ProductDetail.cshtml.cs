using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Adding To Cart");
        var productRes = await catalogService.GetProduct(productId);
        var basket = await basketService.LoadUserBasket();

        basket.Items.Add(new()
        {
            ProductId = productId,
            ProductName = productRes.Name,
            Price = productRes.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketService.StoreBasket(basket);

        return Redirect("ShoppingCart");
    }
}
