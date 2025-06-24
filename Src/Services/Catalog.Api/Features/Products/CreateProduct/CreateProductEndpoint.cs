namespace Catalog.Api.Features.Products.CreateProduct;

internal record CreateProductRequestDto(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

internal record CreateProductResponseDto(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequestDto requestDto, ISender sender) =>
            {
                var command = requestDto.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponseDto>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponseDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}