using Carter;
using MediatR;
using ErrorOr;
using BuildingBlocks.Exceptions.Handler;

namespace Catalog.API.Products.DeleteProduct;

public sealed class DeleteProductCommandEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
        {
            var res = await sender.Send(new DeleteProductCommand(Id));
            return res.Match(
                value => Results.Ok(value),
                error => Results.Problem(error.ToProblemDetail("DeleteProductCommand"))
                );
        })
        .WithName("DeleteProductCommand")
        .Produces<Guid>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product Command")
        .WithDescription("Update Product"); ;
    }
}
