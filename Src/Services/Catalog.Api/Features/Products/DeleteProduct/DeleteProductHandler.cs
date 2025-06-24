using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.DeleteProduct;

internal record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResponse>;

internal record DeleteProductResponse(bool Ok);

internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) 
    :ICommandHandler<DeleteProductCommand, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("new request for DeleteProductHandler: {@command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException();
        
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResponse(true);
    }
}