using MediatR;

namespace BuildingBlocks.CQRS;


// Our Goal Is to make this as an interface:
// internal sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>{}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> where TResponse : notnull
{
}


public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit> where TCommand : ICommand<Unit>
{
}