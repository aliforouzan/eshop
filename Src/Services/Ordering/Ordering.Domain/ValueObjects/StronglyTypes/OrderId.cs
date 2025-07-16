namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record OrderId
{
    public Guid Value { get; } = default!;
}