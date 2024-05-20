using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProducts;

public sealed class GetProductsQueryHandler(IDocumentSession _document) : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var product = await _document.Query<Product>().ToListAsync(cancellationToken);
        return product;
    }
}
