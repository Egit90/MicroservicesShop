using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public sealed record OrderName
{
    public string Value { get; }
    private const int _defaultLength = 5;

    // create
    private OrderName(string value) => Value = value;
    public static OrderName Of(string val)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(val);
        ArgumentOutOfRangeException.ThrowIfNotEqual(val.Length, _defaultLength);
        return new OrderName(val);
    }
}
