using BuildingBlocks.Exceptions;

namespace Basket.Api.Exceptions;

public class BasketNotFoundException(string username) : NotFoundException("basket", username);