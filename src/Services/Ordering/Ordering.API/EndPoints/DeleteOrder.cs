using Carter;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.EndPoints;

public sealed class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/order/{id}", async (Guid Id, ISender sender) =>
        {
            var res = await sender.Send(new DeleteOrderCommand(Id));
            return Results.Ok(res);
        })
            .WithName("Delete Order")
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
    }
}