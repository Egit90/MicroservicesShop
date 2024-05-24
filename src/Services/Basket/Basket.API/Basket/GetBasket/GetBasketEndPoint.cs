
using BuildingBlocks.Exceptions.Handler;
using Carter;
using MediatR;

namespace Basket.API.Basket.GetBasket;

public sealed class GetBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender) =>
        {
            var res = await sender.Send(new GetBasketQuery(username));

            return res.Match(
                value => Results.Ok(value),
                error => Results.Problem(HandledExceptionResponse.Create(error, "GetBasketEndPoint"))
            );
        })
        .WithName("GetBasket")
        .Produces<Guid>(StatusCodes.Status201Created)
        .WithSummary("Get Basket")
        .WithDescription("Get Basket");
    }
}