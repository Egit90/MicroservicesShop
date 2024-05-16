using BuildingBlocks.CQRS;
using Catalog.API.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<Result<Product>>;
