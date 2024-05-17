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

            return res.Match(
                a => Results.Created($"/products/{a}", res),
                b => Results.BadRequest(b)
            );

        })
        .WithName("CreateProduct")
        .Produces<Guid>(StatusCodes.Status201Created)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}