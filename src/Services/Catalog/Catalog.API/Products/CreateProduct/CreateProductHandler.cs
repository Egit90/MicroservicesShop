using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.CreateProduct;

internal sealed class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
	private readonly IDocumentSession _session = session;

	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		// Create Product Entity from the command
		Product product = new()
		{
			Category = command.Category,
			Description = command.Description,
			ImageFile = command.ImageFile,
			Name = command.Name,
			Price = command.Price,
		};

		// Save to DB. using Marten
		_session.Store(product);
		await _session.SaveChangesAsync(cancellationToken);

		// Return CreateProductResult result
		return new CreateProductResult(product.Id);
	}
}