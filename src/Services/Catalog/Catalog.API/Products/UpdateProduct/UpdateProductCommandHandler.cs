using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using ErrorOr;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler(IDocumentSession _document) : ICommandHandler<UpdateProductCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var product = await _document.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            return CustomErrors.ProductNotFound(request.Id);
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
