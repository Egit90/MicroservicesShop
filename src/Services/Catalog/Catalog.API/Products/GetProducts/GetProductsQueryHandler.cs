using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProducts;

public sealed class GetProductsQueryHandler(IDocumentSession _document, ILogger<GetProductsQueryHandler> _logger) : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", request);
        var product = await _document.Query<Product>().ToListAsync(cancellationToken);
        return product;
    }
}
