using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProducts;

public sealed class GetProductsQueryHandler(IDocumentSession Document, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IDocumentSession _document = Document;
    private readonly ILogger<GetProductsQueryHandler> _logger = logger;

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", request);
        var product = await _document.Query<Product>().ToListAsync(cancellationToken);
        return product;
    }
}
