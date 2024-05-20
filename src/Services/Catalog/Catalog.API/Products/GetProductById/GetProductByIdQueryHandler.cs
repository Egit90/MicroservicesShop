using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using ErrorOr;
using Marten;

namespace Catalog.API.Products.GetProductById;

public sealed class GetProductByIdQueryHandler(IDocumentSession _session, ILogger<GetProductByIdQueryHandler> _logger) : IQueryHandler<GetProductByIdQuery, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Looking for the record with id number {@q}", request.Id);
        var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            return CustomErrors.ProductNotFound(request.Id);
        }

        return product;
    }
}
