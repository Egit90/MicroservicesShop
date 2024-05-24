using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public sealed class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull();
        RuleFor(x => x.Cart.UserName).NotNull();
    }
}