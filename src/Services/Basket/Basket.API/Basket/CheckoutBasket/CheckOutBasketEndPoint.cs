using BuildingBlocks.CQRS;
using Carter;
using FluentValidation;
using MediatR;

namespace Basket.API.Basket.CheckoutBasket;

public sealed class CheckOutBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketCommand request, ISender sender) =>
        {
            var res = await sender.Send(request);
            return Results.Ok(res);
        })
        .WithName("CheckOutBasket")
        .Produces<bool>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket");
    }
}
