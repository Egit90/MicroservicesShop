using Carter;
using MediatR;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.EndPoints;

public sealed class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderCommand command, ISender sender) =>
        {
            var res = await sender.Send(command);
            return Results.Ok(res);
        })
            .WithName("Update Order")
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
    }
}