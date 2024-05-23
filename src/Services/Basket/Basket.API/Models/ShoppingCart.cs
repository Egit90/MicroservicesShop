namespace Basket.API.Models;

public sealed class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    public ShoppingCart()
    {

    }
}