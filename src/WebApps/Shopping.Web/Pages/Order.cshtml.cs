using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Ordering;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class OrderListModel(IOrderingService orderingService, ILogger<OrderModel> logger) : PageModel
{

    public IEnumerable<OrderModel> Orders { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
        Orders = await orderingService.GetOrdersByCustomer(CustomerId);

        return Page();

    }
}
