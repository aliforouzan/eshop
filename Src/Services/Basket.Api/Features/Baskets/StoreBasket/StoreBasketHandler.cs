using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;

namespace Basket.Api.Features.Baskets.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart required");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler(
    IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProtoServiceClient
                .GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName },
                    cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}