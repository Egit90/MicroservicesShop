using FluentValidation;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public sealed class UpdateOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}