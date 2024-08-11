using DigitalStore.Core.DTOs;
using FluentValidation;

namespace DigitalStore.Data.Validations;

public class OrderDetailRequestValidation :AbstractValidator<OrderDetailRequestDTO>
{
    public OrderDetailRequestValidation()
    {
     
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id must be a valid GUID.");
        
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId is required.")
            .NotEqual(Guid.Empty).
            WithMessage("OrderId must be a valid GUID.");
        
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("ProductId must be a valid GUID.");
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}