using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler;

public static class HandledExceptionResponse
{

    public static ProblemDetails ToProblemDetail(this List<Error> me, string routeName) => new()
    {
        Title = me.First().Type.ToString(),
        Detail = string.Join(", ", me.Select(x => x.Description)),
        Instance = routeName,
        Status = me.First().Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status400BadRequest,
        }
    };

}