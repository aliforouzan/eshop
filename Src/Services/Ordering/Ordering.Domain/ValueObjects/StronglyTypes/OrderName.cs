namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record OrderName
{
    public string Value { get; } = default!;
}