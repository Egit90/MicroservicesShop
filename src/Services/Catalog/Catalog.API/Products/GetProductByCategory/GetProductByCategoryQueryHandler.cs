using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using LanguageExt;
using LanguageExt.Common;
using Marten;

namespace Catalog.API.Products.GetProductByCategory;

public sealed class GetProductByCategoryQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, Result<IReadOnlyList<Product>>>
{
    private readonly IDocumentSession _session = session;
    private readonly ILogger<GetProductByCategoryQueryHandler> _logger = logger;

    public async Task<Result<IReadOnlyList<Product>>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Trying to find product with category {@cat}", request.Category);

        var product = await _session.Query<Product>()
                                    .Where(x => x.Category.Contains(request.Category))
                                    .ToListAsync(cancellationToken);

        if (product == null)
        {
            return new Result<IReadOnlyList<Product>>(new ProductNotFoundException());
        }

        return new Result<IReadOnlyList<Product>>(product);
    }
}