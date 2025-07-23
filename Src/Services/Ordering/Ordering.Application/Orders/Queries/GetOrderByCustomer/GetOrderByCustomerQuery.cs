namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public record GetOrderByCustomerQuery(Guid CustomerId) : IQuery<GetOrderByCustomerResponse>;
public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrderByCustomerValidator : AbstractValidator<GetOrderByCustomerQuery>
{
    public GetOrderByCustomerValidator()
    {
        RuleFor(o => o.CustomerId).NotEmpty().WithMessage("Customer id is required");
    }
}