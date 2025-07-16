namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string Cvv2 { get; } = default!;
    public int PaymentMethod { get; } = default!;

    private Payment(string cardName, string cardNumber, string expiration, string cvv2, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        Cvv2 = cvv2;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv2, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrEmpty(cardName);
        
        ArgumentException.ThrowIfNullOrEmpty(cardNumber);
        ArgumentOutOfRangeException.ThrowIfNotEqual(cardNumber.Length, 16);
        
        ArgumentException.ThrowIfNullOrEmpty(expiration);
        
        ArgumentException.ThrowIfNullOrEmpty(cvv2);
        ArgumentOutOfRangeException.ThrowIfLessThan(cvv2.Length, 3);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv2.Length, 4);

        return new Payment(cardName, cardNumber, expiration, cvv2, paymentMethod);
    }
}