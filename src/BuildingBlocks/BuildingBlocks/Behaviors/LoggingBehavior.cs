using System.Diagnostics;
using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
   where TRequest : notnull, ICommand<TResponse>
   where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        string requestName = typeof(TRequest).Name;
        string responseName = GetResponseTypeName();

        logger.LogInformation("[START] Handle request={RequestName} -- Response={ResponseName} -- RequestData={RequestData}", requestName, responseName, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();
        timer.Stop();

        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] The request {RequestName} took {TimeTaken} seconds", requestName, timeTaken.Seconds);
        }

        logger.LogInformation("[END] Handled {RequestName} with {ResponseName}", requestName, responseName);
        return response;
    }

    private string GetResponseTypeName()
    {
        var responseType = typeof(TResponse);
        if (responseType.IsGenericType)
        {
            var genericArguments = responseType.GetGenericArguments();
            var genericArgsNames = string.Join(", ", genericArguments.Select(arg => arg.Name));
            return $"{responseType.Name.Split('`')[0]}<{genericArgsNames}>";
        }

        return responseType.Name;
    }
}