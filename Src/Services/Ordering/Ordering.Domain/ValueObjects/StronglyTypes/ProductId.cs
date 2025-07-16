namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record ProductId
{
    public Guid Value { get; } = default!;
}