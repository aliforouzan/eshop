using Mapster;

namespace Ordering.Application.Extension;

public static class OrderExtension
{
    public static IEnumerable<OrderDto> ToOrderDto(this IEnumerable<Order> orders)
    {
        return orders.Select(o =>
        {
            return new OrderDto
            (o.Id.Value,
                o.CustomerId.Value,
                o.OrderName.Value,
                o.ShippingAddress.Adapt<AddressDto>(),
                o.BillingAddress.Adapt<AddressDto>(),
                o.Payment.Adapt<PaymentDto>(),
                o.OrderStatus,
                o.OrderItems.Select(oi =>
                    new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
            );
        });
    }
}