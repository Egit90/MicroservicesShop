using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty();
        RuleFor(x => x.Order.CustomerId).NotEmpty();
        RuleFor(x => x.Order.OrderItems).NotEmpty();
    }
}