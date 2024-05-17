using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using LanguageExt.Common;
using Marten;

namespace Catalog.API.Products.DeleteProduct;

public sealed class DeleteProductCommandHandler(IDocumentSession _session, ILogger<DeleteProductCommandHandler> _logger) : ICommandHandler<DeleteProductCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting The Record With Id {@Id}", request.Id);
        var products = await _session.LoadAsync<Product>(request.Id, cancellationToken);


        if (products == null)
        {
            return new Result<bool>(new ProductNotFoundException(request.Id));
        }

        _session.Delete(products);
        await _session.SaveChangesAsync(cancellationToken);

        return true;
    }
}
