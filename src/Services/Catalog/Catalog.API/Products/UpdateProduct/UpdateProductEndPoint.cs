using BuildingBlocks.Exceptions.Handler;
using Carter;
using MediatR;

namespace Catalog.API.Products.UpdateProduct;

public sealed class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("products", async (UpdateProductCommand command, ISender sender) =>
        {
            var res = await sender.Send(command);

            return res.Match(
                a => Results.Ok(a),
                error => Results.Problem(HandledExceptionResponse.Create(error, "UpdateProductEndPoint"))
            );
        })
        .WithName("UpdateProductEndPoint")
        .Produces<Guid>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
