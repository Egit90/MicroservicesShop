using FluentValidation;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public sealed class GetOrdersByCustomerQueryValidator : AbstractValidator<GetOrdersByCustomerQuery>
{
    public GetOrdersByCustomerQueryValidator()
    {
        RuleFor(x => x.customer).NotEmpty();
    }
}