using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using LanguageExt.Common;

namespace Catalog.API.Products.GetProductByCategory;

public sealed record GetProductByCategoryQuery(string Category) : IQuery<Result<IReadOnlyList<Product>>>;

