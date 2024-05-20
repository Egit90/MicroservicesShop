using BuildingBlocks.Exceptions.Handler;
using Carter;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public sealed class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var res = await sender.Send(new GetProductByCategoryQuery(category));

            return res.Match(
                value => Results.Ok(value),
             error => Results.Problem(HandledExceptionResponse.Create(error, "GetProductByCategory"))
            );
        })
        .WithName("GetProductByCategory")
        .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
