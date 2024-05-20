using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler;

public static class HandledExceptionResponse
{
    public static ProblemDetails Create(List<Error> errorList, string Instance) => new()
    {
        Title = errorList.First().Type.ToString(),
        Detail = string.Join(", ", errorList.Select(x => x.Description)),
        Instance = Instance,
        Status = errorList.First().Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status400BadRequest,
        }
    };

}