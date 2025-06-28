using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.GetProductsByCategory;

internal record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResponse>;

internal record GetProductsByCategoryResponse(IEnumerable<Product> Products);

internal class GetProductsByCategoryHandler(IDocumentSession session) 
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResponse>
{
    public async Task<GetProductsByCategoryResponse> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResponse(products);
    }
}