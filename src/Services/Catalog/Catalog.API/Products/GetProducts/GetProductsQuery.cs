using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

public sealed record GetProductsQuery() : IQuery<IEnumerable<Product>>;
