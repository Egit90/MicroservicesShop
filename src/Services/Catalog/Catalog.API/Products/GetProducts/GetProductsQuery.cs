using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Products.GetProducts;

public sealed record GetProductsQuery() : IQuery<IEnumerable<Product>>;
