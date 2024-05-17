using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var result = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var fail = result
                    .Where(x => x.Errors.Count > 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

        if (fail.Count > 0) throw new ValidationException(fail);

        return await next();
    }
}