using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Marten.Pagination;

namespace Catalog.Api.Features.Products.GetProducts;

internal record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResponse>;

internal record GetProductsResponse(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber, query.PageSize);
        return new GetProductsResponse(products);
    }
}