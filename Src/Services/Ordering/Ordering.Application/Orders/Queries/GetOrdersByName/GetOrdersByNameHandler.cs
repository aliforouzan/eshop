using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extension;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetOrdersByNameQuery, GetOrderByNameResponse>
{
    public async Task<GetOrderByNameResponse> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Equals(query.OrderName))
            .ToListAsync(cancellationToken);

        return new GetOrderByNameResponse(order.ToOrderDto());
    }
}