namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(o => o.Price * o.Quantity);
        private set { }
    }

    private Order(OrderId orderId, CustomerId customerId, OrderName orderName,
        Address shippingAddress, Address billingAddress, Payment payment, OrderStatus orderStatus)
    {
        Id = orderId;
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = orderStatus;
    }

    public static Order Create(OrderId orderId, CustomerId customerId, OrderName orderName,
        Address shippingAddress, Address billingAddress, Payment payment, OrderStatus orderStatus)
    {
        var order = new Order(orderId, customerId, orderName, shippingAddress, billingAddress, payment, orderStatus);

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress,
        Payment payment, OrderStatus orderStatus)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = orderStatus;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void AddItem(ProductId productId, int quantity, decimal price)
    {
        var item = OrderItem.Create(Id, productId, quantity, price);
        _orderItems.Add(item);
    }
    
    public void RemoveItem(ProductId productId)
    {
        var item = OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

        if (item is not null)
        {
            _orderItems.Remove(item);
        }
    }
}