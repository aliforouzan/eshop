namespace Basket.Api.Features.Baskets.DeleteBasket;

public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult();

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x => x.Username).NotNull().WithMessage("Username is required");
    }
}
public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}