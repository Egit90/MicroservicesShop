using BuildingBlocks.Exceptions;
using ErrorOr;

namespace Basket.API.Exceptions;

public static class CustomErrors
{
    public static Error BasketNotFound(string username)
    {
        return Error.NotFound(description: $"Basket With username of {username} was not found in the db");
    }
}