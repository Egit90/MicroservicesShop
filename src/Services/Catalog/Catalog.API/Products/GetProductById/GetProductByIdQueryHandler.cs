using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using ErrorOr;
using Marten;

namespace Catalog.API.Products.GetProductById;

public sealed class GetProductByIdQueryHandler(IDocumentSession _session) : IQueryHandler<GetProductByIdQuery, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            return CustomErrors.ProductNotFound(request.Id);
        }

        return product;
    }
}
