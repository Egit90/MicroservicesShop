using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.StoreBasket;
public record StoreBasketRequest(ShoppingCart Cart);
public sealed class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();


            var res = await sender.Send(command);
            return Results.Created($"/basket/{res}", res);
        })
                .WithName("StoreBasketEndPoint")
        .Produces<Guid>(StatusCodes.Status201Created)
        .WithSummary("Store Basket")
        .WithDescription("Store Basket");
    }
}
