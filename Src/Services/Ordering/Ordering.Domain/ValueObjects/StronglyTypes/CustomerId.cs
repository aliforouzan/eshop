namespace Ordering.Domain.ValueObjects.StronglyTypes;

public record CustomerId
{
    public Guid Value { get; } = default!;
    private CustomerId(Guid guid) => Value = guid;

    public static CustomerId Of(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new DomainException("Customer id shouldn't be empty");
        }

        return new CustomerId(guid);
    }
}