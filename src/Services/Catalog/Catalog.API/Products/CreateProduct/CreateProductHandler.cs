using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using LanguageExt.Common;
using Marten;

namespace Catalog.API.Products.CreateProduct;

internal sealed class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, Result<Guid>>
{
	public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
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

		// Return CreateProductResult result
		return product.Id;
	}
}