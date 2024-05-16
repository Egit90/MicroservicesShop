using Carter;
using Catalog.API.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Products.GetProductById;

public sealed class GetProductByIdEndPoint() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (string Id, ISender Sender) =>
        {
            if (Guid.TryParse(Id, out Guid Id2))
            {
                var products = await Sender.Send(new GetProductByIdQuery(Id2));
                return products.Match(x => Results.Ok(x), e =>
                {
                    if (e.Message == "Not Found") return Results.NotFound("");
                    return TypedResults.BadRequest(e.Message);
                });
            }
            return Results.BadRequest("Id Must be a Guid");
        })
         .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
