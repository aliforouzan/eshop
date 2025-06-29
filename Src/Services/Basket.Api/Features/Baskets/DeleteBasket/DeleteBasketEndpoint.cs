namespace Basket.Api.Features.Baskets.DeleteBasket;

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Basket/{username}", async (string username, ISender sender) =>
        {
            await sender.Send(new DeleteBasketCommand(username));
            return Results.Ok();
        })
        .WithName("DeleteBasket")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket");
    }
}