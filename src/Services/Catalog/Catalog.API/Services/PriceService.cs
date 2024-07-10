using Catalog.API.Models;
using Grpc.Core;
using Marten;

namespace Catalog.API.Services;

public sealed class PriceService(IDocumentSession _session) : PriceProtoService.PriceProtoServiceBase
{
    public override async Task<PricesResponse> GetPrices(GetPricesRequest request, ServerCallContext context)
    {
        var productIdList = request.ProductIdList.ToList().Select(x => new Guid(x)).ToList();
        var prices = new PricesResponse(); // new List<PriceInfo>();
        var Products = await _session.LoadManyAsync<Product>(productIdList);

        foreach (var Product in Products)
        {
            prices.Prices.Add(new PriceInfo
            {
                Id = Product.Id.ToString(),
                Price = (double)Product.Price,
            });
        }

        return prices;
    }
}