using FluentValidation;

namespace Basket.API.Basket.CheckoutBasket;

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.BasketCheckOutDto).NotNull();
        RuleFor(x => x.BasketCheckOutDto.UserName).NotNull();

    }
}