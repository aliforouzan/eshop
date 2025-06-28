using System.Data;
using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResponse>;

public record UpdateProductResponse(bool Ok);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(255).WithMessage("Name length should be less than 255 character");
        RuleFor(x => x.Category).NotEmpty().WithMessage("product should be in a category at least!");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be more than 0!");
    }
}

internal class UpdateProductHandler(IDocumentSession session) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException(command.Id);
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResponse(true);
    }
}