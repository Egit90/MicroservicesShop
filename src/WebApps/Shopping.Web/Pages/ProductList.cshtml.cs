using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Extensions;
using Shopping.Web.Models.Basket;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;
public class ProductListModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger) : PageModel
{
    public IEnumerable<string> CategoryList { get; set; } = [];
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    [BindProperty(SupportsGet = true)] public string SelectedCategory { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        var res = await catalogService.GetProducts();
        CategoryList = res.SelectMany(p => p.Category).Distinct();

        if (!string.IsNullOrEmpty(categoryName))
        {
            ProductList = res.Where(p => p.Category.Contains(categoryName));
        }
        else
        {
            ProductList = res;
        }
        return Page();
    }


    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Adding To Cart");

        var basket = await basketService.LoadUserBasket();

        var isAdded = await basket.AddToCart(productId, catalogService);

        var request = new StoreBasketRequest(basket);

        await basketService.StoreBasket(request);

        return RedirectToPage("Cart");
    }
}
