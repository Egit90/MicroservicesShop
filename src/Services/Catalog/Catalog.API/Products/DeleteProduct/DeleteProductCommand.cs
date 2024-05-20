using BuildingBlocks.CQRS;
using ErrorOr;

namespace Catalog.API.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : ICommand<ErrorOr<bool>>;
