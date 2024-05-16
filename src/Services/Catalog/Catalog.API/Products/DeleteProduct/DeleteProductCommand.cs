using BuildingBlocks.CQRS;
using LanguageExt.Common;

namespace Catalog.API.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : ICommand<Result<bool>>;
