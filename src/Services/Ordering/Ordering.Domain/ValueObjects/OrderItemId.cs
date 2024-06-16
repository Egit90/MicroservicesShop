using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public sealed record OrderItemId
{
    public Guid Value { get; }

    // create
    private OrderItemId(Guid value) => Value = value;
    public static OrderItemId Of(Guid val)
    {
        ArgumentNullException.ThrowIfNull(val);
        if (val == Guid.Empty) throw new DomainException("OrderItemId cant be empty");
        return new OrderItemId(val);
    }
}