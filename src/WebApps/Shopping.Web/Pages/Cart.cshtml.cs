using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
{
    public ShoppingCartModel Cart { get; set; } = new();
    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.LoadUserBasket();
        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
    {
        logger.LogInformation("Removing Cart");
        var cart = await basketService.LoadUserBasket();

        cart.Items.RemoveAll(x => x.ProductId == productId);

        await basketService.StoreBasket(new StoreBasketRequest(cart));
        return RedirectToPage();
    }


}