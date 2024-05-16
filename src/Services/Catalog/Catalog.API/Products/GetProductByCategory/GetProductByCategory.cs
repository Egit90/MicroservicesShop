using BuildingBlocks.CQRS;
using Carter;
using Catalog.API.Models;
using LanguageExt.Common;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public sealed record GetProductByCategoryQuery(string Category) : IQuery<Result<IReadOnlyList<Product>>>;


public sealed class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{category}", async (string category, ISender sender) =>
        {
            var res = await sender.Send(new GetProductByCategoryQuery(category));

            return res.Match(
                a => Results.Ok(a),
                b => Results.NotFound(b.Message)
            );
        });
    }
}