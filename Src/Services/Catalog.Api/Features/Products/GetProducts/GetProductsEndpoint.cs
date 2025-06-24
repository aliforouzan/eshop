using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.GetProducts;

public record GetProductsResponseDto(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var products = result.Adapt<GetProductsResponseDto>();

            return Results.Ok(products);
        })
        .WithName("GetProduct")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}