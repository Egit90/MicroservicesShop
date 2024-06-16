using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public sealed record ProductId
{
    public Guid Value { get; }

    // create
    private ProductId(Guid value) => Value = value;
    public static ProductId Of(Guid val)
    {
        ArgumentNullException.ThrowIfNull(val);
        if (val == Guid.Empty) throw new DomainException("ProductId cant be empty");
        return new ProductId(val);
    }
}
