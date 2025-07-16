namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record OrderItemId
{
    public Guid Value { get; } = default!;
}