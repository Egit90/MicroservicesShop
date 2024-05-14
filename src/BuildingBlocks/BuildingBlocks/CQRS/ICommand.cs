using MediatR;

namespace BuildingBlocks.CQRS;


// Our Goal Is to make this as an interface:
// using MediatR;
// namespace Catalog.API.Products.CreateProduct;
// public record CreateProductCommand(string Name) : IRequest<CreateProductResult>;


public interface ICommand<out TResponse> : IRequest<TResponse>
{
}


public interface ICommand : ICommand<Unit>
{
}