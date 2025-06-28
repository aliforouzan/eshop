using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.GetProducts;

internal record GetProductsQuery : IQuery<GetProductsResponse>;

internal record GetProductsResponse(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResponse(products);
    }
}