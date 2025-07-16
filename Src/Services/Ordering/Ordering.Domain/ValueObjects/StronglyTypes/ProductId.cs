namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record ProductId
{
    public Guid Value { get; } = default!;
    private ProductId(Guid guid) => Value = guid;

    public static ProductId Of(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new DomainException("Product item id shouldn't be empty");
        }

        return new ProductId(guid);
    }
}