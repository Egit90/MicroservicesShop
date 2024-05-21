using Carter;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.GetProducts;

public sealed class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsQuery request, ISender sender) =>
        {
            var res = await sender.Send(request);
            return Results.Ok(res);
        })
        .WithName("GetProducts")
        .Produces<IEnumerable<Product>>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("GetProducts");
    }
}
