namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record OrderItemId
{
    public Guid Value { get; } = default!;
    private OrderItemId(Guid guid) => Value = guid;

    public static OrderItemId Of(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new DomainException("Order item id shouldn't be empty");
        }

        return new OrderItemId(guid);
    }
}