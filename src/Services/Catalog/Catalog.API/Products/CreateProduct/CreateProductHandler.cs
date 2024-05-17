using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using LanguageExt.Common;
using Marten;

namespace Catalog.API.Products.CreateProduct;

internal sealed class CreateProductHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
					: ICommandHandler<CreateProductCommand, Result<Guid>>
{
	public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{

		var res = await validator.ValidateAsync(command, cancellationToken);

		if (!res.IsValid)
		{
			string ErrorMessage = string.Join(", ", res.Errors.Select(x => x.ErrorMessage).ToList());
			return new Result<Guid>(new ValidationException(ErrorMessage));
		}


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
		session.Store(product);
		await session.SaveChangesAsync(cancellationToken);

		// Return CreateProductResult result
		return product.Id;
	}
}