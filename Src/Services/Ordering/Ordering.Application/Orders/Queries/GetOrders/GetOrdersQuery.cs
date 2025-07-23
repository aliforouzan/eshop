using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest Pagination) : IQuery<GetOrdersResponse>;
public record GetOrdersResponse(PaginationResult<OrderDto> Result);