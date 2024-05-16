using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using LanguageExt.Common;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler(
    IDocumentSession document,
    ILogger<UpdateProductCommandHandler> logger
) : ICommandHandler<UpdateProductCommand, Result<Guid>>
{
    private readonly IDocumentSession _document = document;
    private readonly ILogger<UpdateProductCommandHandler> _logger = logger;

    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating Record With Id Of {@Id}", request.Id);

        var product = await _document.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            return new Result<Guid>(new ProductNotFoundException());
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
