using Catalog.Api.Features.Products.CreateProduct;

namespace Catalog.Api.Features.Products.UpdateProduct;

public record UpdateProductRequestDto(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (Guid id, UpdateProductRequestDto requestDto, ISender sender) =>
            {
                var command = new UpdateProductCommand(id, requestDto.Name, requestDto.Category, requestDto.Description,
                    requestDto.ImageFile, requestDto.Price);
                
                requestDto.Adapt(command);
                await sender.Send(command);
                return Results.Ok();
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
    }
}