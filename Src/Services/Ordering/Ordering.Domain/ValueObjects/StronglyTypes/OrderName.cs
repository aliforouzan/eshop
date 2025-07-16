namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record OrderName
{
    private static int _minOrderNameLen = 5;
    private static int _maxOrderNameLen = 5;
    public string Value { get; } = default!;
    private OrderName(string name) => Value = name;

    public static OrderName Of(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, _minOrderNameLen);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, _maxOrderNameLen);

        return new OrderName(name);
    }
}