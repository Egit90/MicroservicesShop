using FluentValidation;

namespace Ordering.Application.Orders.Queries;

public sealed class GetOrdersByNameQueryValidator : AbstractValidator<GetOrdersByNameQuery>
{
    public GetOrdersByNameQueryValidator()
    {
        RuleFor(x => x.orderName).NotEmpty();
    }
}