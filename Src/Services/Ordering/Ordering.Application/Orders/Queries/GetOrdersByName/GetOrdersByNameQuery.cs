namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrderByNameResponse>;
public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrderByNameValidator : AbstractValidator<GetOrdersByNameQuery>
{
    public GetOrderByNameValidator()
    {
        RuleFor(o => o.OrderName).NotEmpty().WithMessage("order name is required!");
    }
}