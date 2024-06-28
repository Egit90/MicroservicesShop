using Basket.API.Dtos;
using Carter;
using FluentValidation;
using MediatR;

namespace Basket.API.Basket.CheckoutBasket;
public record CheckoutBasketRequest(BasketCheckOutDto BasketCheckoutDto);

public sealed class CheckOutBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            var cmd = new CheckoutBasketCommand(request.BasketCheckoutDto);
            var res = await sender.Send(cmd);
            return Results.Ok(res);
        })
        .WithName("CheckOutBasket")
        .Produces<bool>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket");
    }
}
