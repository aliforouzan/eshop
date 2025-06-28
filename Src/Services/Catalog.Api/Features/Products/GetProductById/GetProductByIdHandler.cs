using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.GetProductById;

internal record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResponse>;

internal record GetProductByIdResponse(Product Product);

internal class GetProductByIdHandler(IDocumentSession session) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResponse(product);
    }
}