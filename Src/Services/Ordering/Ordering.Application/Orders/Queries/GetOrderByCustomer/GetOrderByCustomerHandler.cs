using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extension;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResponse>
{
    public async Task<GetOrderByCustomerResponse> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId.Value.Equals(query.CustomerId))
            .ToListAsync(cancellationToken);

        return new GetOrderByCustomerResponse(orders.ToOrderDto());
    }
}