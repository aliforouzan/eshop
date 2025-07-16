namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record CustomerId
{
    public Guid Value { get; } = default!;
}