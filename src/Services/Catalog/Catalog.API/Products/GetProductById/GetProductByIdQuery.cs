using BuildingBlocks.CQRS;
using Catalog.API.Models;
using ErrorOr;

namespace Catalog.API.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<ErrorOr<Product>>;
