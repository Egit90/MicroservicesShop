using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using LanguageExt;
using LanguageExt.Common;
using Marten;

namespace Catalog.API.Products.GetProductByCategory;

public sealed class GetProductByCategoryQueryHandler(
    IDocumentSession _session,
    ILogger<GetProductByCategoryQueryHandler> _logger) : IQueryHandler<GetProductByCategoryQuery, Result<IReadOnlyList<Product>>>
{
    public async Task<Result<IReadOnlyList<Product>>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Trying to find product with category {@cat}", request.Category);

        var product = await _session.Query<Product>()
                                    .Where(x => x.Category.Contains(request.Category))
                                    .ToListAsync(cancellationToken);

        if (product == null || product.Count == 0)
        {
            return new Result<IReadOnlyList<Product>>(new ProductNotFoundException());
        }

        return new Result<IReadOnlyList<Product>>(product);
    }
}