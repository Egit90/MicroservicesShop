using BuildingBlocks.Exceptions;
using ErrorOr;

namespace Catalog.API.Exceptions;

public static class CustomErrors
{
    public static Error ProductNotFound(Guid id)
    {
        return Error.NotFound(description: $"Product With id of {id} was not found in the db");
    }
    public static Error ProductNotFound(string category)
    {
        return Error.NotFound(description: $"Product With category of {category} was not found in the db");
    }
}