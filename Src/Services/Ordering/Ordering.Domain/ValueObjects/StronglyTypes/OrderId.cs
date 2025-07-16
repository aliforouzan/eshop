namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record OrderId
{
    public Guid Value { get; } = default!;
    private OrderId(Guid guid) => Value = guid;

    public static OrderId Of(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new DomainException("Order id shouldn't be empty");
        }

        return new OrderId(guid);
    }
}