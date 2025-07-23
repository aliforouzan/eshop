using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extension;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResponse>
{
    public async Task<GetOrdersResponse> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.Pagination.PageIndex;
        var pageSize = query.Pagination.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrdersResponse(
            new PaginationResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ToOrderDto())
            );
    }
}