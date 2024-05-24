using FluentValidation;

namespace Basket.API.Basket.DeleteBasket;

public sealed class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty();
    }
}
