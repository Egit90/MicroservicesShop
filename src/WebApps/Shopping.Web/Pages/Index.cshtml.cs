using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class IndexModel(ICatalogService catalogService, ILogger<IndexModel> logger) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {

        logger.LogInformation("Index Paged");
        ProductList = await catalogService.GetProducts();

        return Page();
    }
}