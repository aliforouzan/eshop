using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.CreateProduct;

internal record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResponse>;

internal record CreateProductResponse(Guid Id);

internal class CreateProductHandler(IDocumentSession session, ILogger<CreateProductHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("new request for CreateProductHandler: {@command}", command);
        var product = command.Adapt<Product>();
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return product.Adapt<CreateProductResponse>();
    }
}