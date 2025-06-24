using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.GetProductsByCategory;

public record GetProductsByCategoryResponseDto(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/categories/{category}", async (string category, ISender sender) =>
        {
            var query = new GetProductsByCategoryQuery(category);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsByCategoryResponseDto>();

            return Results.Ok(response);
        })
        .WithName("GetProductsByCategory")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}