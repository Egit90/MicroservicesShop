using Carter;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.EndPoints;

public sealed class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId:guid}", async (Guid CustomerId, ISender sender) =>
        {
            var orders = await sender.Send(new GetOrdersByCustomerQuery(CustomerId));
            return Results.Ok(orders);
        })
            .WithName("GetOrdersByCustomerId")
            .Produces<IEnumerable<OrderDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By CustomerId")
            .WithDescription("Get Orders By CustomerId");
    }
}