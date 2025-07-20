using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(object key) : base("order", key)
    {
    }
}