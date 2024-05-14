using Carter;
using MediatR;

namespace Catalog.API.Products.CreateProduct;

public sealed class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductCommand command, ISender sender) =>
        {
            var res = await sender.Send(command);
            return Results.Created($"/products/{res.ID}", res);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResult>(StatusCodes.Status201Created)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}