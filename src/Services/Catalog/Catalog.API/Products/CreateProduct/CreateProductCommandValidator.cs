using FluentValidation;

namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Must Not Be Empty");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Must Not be Null");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Name Must Not Be Empty");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Name Must Not Be Empty");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must be More Than 0");
    }
}
