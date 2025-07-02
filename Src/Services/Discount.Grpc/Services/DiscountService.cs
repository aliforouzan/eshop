using Discount.Grpc.Data;
using Discount.Grpc.Models;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext discountContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountContext
            .Coupons
            .FirstOrDefaultAsync(c => c.ProductName.Equals(request.ProductName));

        if (coupon is null)
            coupon = new Coupon { Id = 0, ProductName = "NoDiscount", Description = "No Discount", Amount = 0 };

        logger.LogInformation("Discount value for {req}: {amount}", request.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        discountContext.Coupons.Add(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation("New Coupon: ProductName: {Name}, Description: {Description}, Amount: {Amount}",
            coupon.ProductName, coupon.Description, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        discountContext.Update(coupon);
        await discountContext.SaveChangesAsync();
        
        logger.LogInformation(
            "Update Coupon: ProductName: {Name} -> {NewName}, " +
            "Description: {Description} -> {NewDescription}, " +
            "Amount: {Amount} -> {NewAmount}",
            request.ProductName, coupon.ProductName,
            request.Description, request.Description,
            request.Amount, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = await discountContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));

        discountContext.Coupons.Remove(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation("Delete Coupon: Id: {Id} ProductName: {Name}, Description: {Description}, Amount: {Amount}",
            coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount);
        
        return new DeleteDiscountResponse {Success = true};
    }
}