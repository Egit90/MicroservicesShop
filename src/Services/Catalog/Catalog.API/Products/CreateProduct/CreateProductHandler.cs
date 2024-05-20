using BuildingBlocks.CQRS;
using Catalog.API.Models;
using ErrorOr;
using Marten;

namespace Catalog.API.Products.CreateProduct;

internal sealed class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, ErrorOr<Guid>>
{
	public async Task<ErrorOr<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		Product product = new()
		{
			Category = command.Category,
			Description = command.Description,
			ImageFile = command.ImageFile,
			Name = command.Name,
			Price = command.Price,
		};

		// Save to DB. using Marten
		session.Store(product);
		await session.SaveChangesAsync(cancellationToken);

		// Return CreateProductErrorOr result
		return product.Id;
	}
}