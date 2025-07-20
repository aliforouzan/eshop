namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) 
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateOrder(command.Order);
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateOrder(OrderDto orderDto)
    {
        var shippingDto = orderDto.ShippingAddress;
        var shipping = Address.Of(shippingDto.FirstName, shippingDto.LastName, shippingDto.EmailAddress,
            shippingDto.AddressLine, shippingDto.Country, shippingDto.State, shippingDto.ZipCode);

        var billingDto = orderDto.BillingAddress;
        var billing = Address.Of(billingDto.FirstName, billingDto.LastName, billingDto.EmailAddress,
            billingDto.AddressLine, billingDto.Country, billingDto.State, billingDto.ZipCode);

        /*
         TODO: check this: is it possible?
         billing = billingDto.Adapt<Address>();
        */

        var paymentDto = orderDto.Payment;
        var payment = Payment.Of(paymentDto.CardName, paymentDto.CardNumber, paymentDto.Expiration, paymentDto.Cvv2,
            paymentDto.PaymentMethod);

        var order = Order.Create(
            OrderId.Of(orderDto.Id),
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName),
            shipping,
            billing,
            payment
        );

        return order;
    }
}