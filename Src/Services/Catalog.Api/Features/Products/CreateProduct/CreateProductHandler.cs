using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResponse>;

public record CreateProductResponse(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
        RuleFor(x => x.Category).NotEmpty().WithMessage("At least one category is required!");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be more than 0!");
    }
}

internal class CreateProductHandler(
    IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = command.Adapt<Product>();
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return product.Adapt<CreateProductResponse>();
    }
}