using Carter;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;

public sealed class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var res = await sender.Send(new DeleteBasketCommand(username));

            return res ? Results.Ok(res)
                        : Results.BadRequest(res);
        })
        .WithName("DeleteBasketEndPoint")
        .Produces<Guid>(StatusCodes.Status201Created)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket"); ;
    }
}
