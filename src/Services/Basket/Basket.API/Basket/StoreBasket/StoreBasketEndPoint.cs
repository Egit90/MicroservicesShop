using Carter;
using MediatR;

namespace Basket.API.Basket.StoreBasket;

public sealed class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketCommand request, ISender sender) =>
        {
            var res = await sender.Send(request);

            return Results.Created($"/basket/{res}", res);
        })
                .WithName("StoreBasketEndPoint")
        .Produces<Guid>(StatusCodes.Status201Created)
        .WithSummary("Store Basket")
        .WithDescription("Store Basket");
    }
}
