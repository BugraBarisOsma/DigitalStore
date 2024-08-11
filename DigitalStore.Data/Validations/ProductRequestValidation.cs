using DigitalStore.Core.DTOs;
using FluentValidation;

namespace DigitalStore.Data.Validations;

public class ProductRequestValidation : AbstractValidator<ProductRequestDTO>
{
    public ProductRequestValidation()
    {
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .Length(3, 100)
            .WithMessage("Product name must be between 3 and 100 characters.");
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
        
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock cannot be negative.");
        
        RuleFor(x => x.RewardPointsPercentage)
            .InclusiveBetween(0, 100)
            .WithMessage("Reward points percentage must be between 0 and 100.");
      
        RuleFor(x => x.MaxRewardPoints)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Max reward points cannot be negative.");
        
        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("At least one category ID is required.")
            .ForEach(id => id.NotEqual(Guid.Empty)
                .WithMessage("Category ID must be a valid GUID."));
    }
}