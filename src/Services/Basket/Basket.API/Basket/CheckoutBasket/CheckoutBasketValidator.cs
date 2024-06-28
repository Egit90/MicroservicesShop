using Basket.API.Dtos;
using FluentValidation;

namespace Basket.API.Basket.CheckoutBasket;

public class CheckoutBasketValidator : AbstractValidator<BasketCheckOutDto>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.UserName).NotNull();
        RuleFor(x => x.CardName).NotNull();

    }
}