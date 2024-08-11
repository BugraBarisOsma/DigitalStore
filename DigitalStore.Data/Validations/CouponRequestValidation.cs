using DigitalStore.Core.DTOs;
using FluentValidation;

namespace DigitalStore.Data.Validations;

public class CouponRequestValidation : AbstractValidator<CouponRequestDTO>
{
    public CouponRequestValidation()
    {
     
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required.")
            .Length(3, 10).WithMessage("Coupon code must be between 3 and 10 characters.")
            .Matches(@"^\S*$").WithMessage("Coupon code cannot contain whitespace.");
        
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        
        RuleFor(x => x.ExpiryDate)
            .GreaterThan(DateTime.Now).WithMessage("Expiry date must be a future date.");
        
        RuleFor(x => x.IsUsed)
            .Equal(false).WithMessage("IsUsed must be false for a new coupon.");
    }
}