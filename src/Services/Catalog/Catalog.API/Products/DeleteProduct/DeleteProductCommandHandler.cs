using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;
using ErrorOr;

namespace Catalog.API.Products.DeleteProduct;

public sealed class DeleteProductCommandHandler(IDocumentSession _session) : ICommandHandler<DeleteProductCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var products = await _session.LoadAsync<Product>(request.Id, cancellationToken);


        if (products == null)
        {
            return CustomErrors.ProductNotFound(request.Id);
        }

        _session.Delete(products);
        await _session.SaveChangesAsync(cancellationToken);

        return true;
    }
}
