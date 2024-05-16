using Carter;
using MediatR;

namespace Catalog.API.Products.DeleteProduct;

public sealed class DeleteProductCommandEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
        {
            var res = await sender.Send(new DeleteProductCommand(Id));
            return res.Match(
                a => Results.Ok(a),
                b => Results.NotFound(b.Message)
            );
        })
        .WithName("DeleteProductCommand")
        .Produces<Guid>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product Command")
        .WithDescription("Update Product"); ;
    }
}
