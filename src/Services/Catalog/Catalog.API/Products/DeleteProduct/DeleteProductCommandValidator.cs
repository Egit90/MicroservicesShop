using FluentValidation;

namespace Catalog.API.Products.DeleteProduct;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty().WithMessage("Id Can't be empty..");
    }
}
