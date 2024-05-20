using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Must Not Be Empty").MinimumLength(4);
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Must Not be Null");
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.ImageFile).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
