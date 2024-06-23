using Carter;
using MediatR;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.EndPoints;

public sealed class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderCommand request, ISender sender) =>
        {
            var res = await sender.Send(request);

            return Results.Created($"/orders/{res}", res);
        })
            .WithName("Create Order")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
    }
}
