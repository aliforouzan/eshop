using Catalog.Api.Features.Products.GetProducts;
using Catalog.Api.Models;

namespace Catalog.Api.Features.Products.GetProductById;

public record GetProductByIdResponseDto(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductByIdResponseDto>();

            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}