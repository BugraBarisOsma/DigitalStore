using DigitalStore.Core.DTOs;
using FluentValidation;

namespace DigitalStore.Data.Validations;

public class CategoryRequestValidation : AbstractValidator<CategoryRequestDTO>
{
    public CategoryRequestValidation()
    {
       
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .Length(3, 50).WithMessage("Category name must be between 3 and 50 characters.");

   
        RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage("URL is required.");
           
        
        RuleFor(x => x.Tag)
            .Length(2, 50).WithMessage("Tag must be between 2 and 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.Tag));
    }
}