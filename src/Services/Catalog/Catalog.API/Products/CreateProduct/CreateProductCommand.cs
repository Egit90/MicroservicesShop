using BuildingBlocks.CQRS;
using ErrorOr;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
     string Name,
     List<string> Category,
     string Description,
     string ImageFile,
     decimal Price
) : ICommand<ErrorOr<Guid>>;
