using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.GetProducts;

public record GetProductsRequestDto(int? PageNumber = 1, int? PageSize = 10);

public record GetProductsResponseDto(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequestDto request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
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