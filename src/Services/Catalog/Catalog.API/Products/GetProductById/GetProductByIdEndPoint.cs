using BuildingBlocks.Exceptions.Handler;
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
                return products.Match(
                        value => Results.Ok(value),
                        error => Results.Problem(HandledExceptionResponse.Create(error, "GetProductById"))
                        );
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
