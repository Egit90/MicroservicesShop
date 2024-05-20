using BuildingBlocks.CQRS;
using Catalog.API.Models;
using ErrorOr;

namespace Catalog.API.Products.GetProductByCategory;

public sealed record GetProductByCategoryQuery(string Category) : IQuery<ErrorOr<IReadOnlyList<Product>>>;

