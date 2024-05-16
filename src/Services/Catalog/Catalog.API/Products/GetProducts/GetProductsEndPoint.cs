using Carter;
using Catalog.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProducts;

public sealed class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var res = await sender.Send(new GetProductsQuery());
            return Results.Ok(res);
        })
        .WithName("GetProducts")
        .Produces<IEnumerable<Product>>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("GetProducts");
    }
}
