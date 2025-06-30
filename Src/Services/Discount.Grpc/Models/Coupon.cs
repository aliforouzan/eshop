namespace Discount.Grpc.Models;

public class Coupon
{
    public int Id { set; get; }
    public string ProductName { set; get; } = default!;
    public string Description { set; get; } = default!;
    public int Amount { set; get; }
}