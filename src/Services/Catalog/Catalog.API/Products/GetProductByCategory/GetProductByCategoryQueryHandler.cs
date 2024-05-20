using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using ErrorOr;
using Marten;

namespace Catalog.API.Products.GetProductByCategory;

public sealed class GetProductByCategoryQueryHandler(
    IDocumentSession _session
  ) : IQueryHandler<GetProductByCategoryQuery, ErrorOr<IReadOnlyList<Product>>>
{
    public async Task<ErrorOr<IReadOnlyList<Product>>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {

        var product = await _session.Query<Product>()
                                    .Where(x => x.Category.Contains(request.Category))
                                    .ToListAsync(cancellationToken);

        if (product == null || product.Count == 0)
        {
            return CustomErrors.ProductNotFound(request.Category);
        }

        return product.ToArray();
    }
}