using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using LanguageExt.Common;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler(
    IDocumentSession _document,
    ILogger<UpdateProductCommandHandler> _logger
) : ICommandHandler<UpdateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating Record With Id Of {@Id}", request.Id);

        var product = await _document.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            return new Result<Guid>(new ProductNotFoundException(request.Id));
        }
        product.Name = request.Name;
        product.Category = request.Category;
        product.Description = request.Description;
        product.ImageFile = request.ImageFile;
        product.Price = request.Price;


        _document.Update(product);
        await _document.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
