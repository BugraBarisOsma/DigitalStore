using DigitalStore.Core.DTOs;
using FluentValidation;

namespace DigitalStore.Data.Validations;

public class UserRequestValidation : AbstractValidator<UserRequestDTO>
{
    public UserRequestValidation()
    {
        // Username is required and must be between 3 and 50 characters
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        // Surname is required and must be between 3 and 50 characters
        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .Length(3, 50).WithMessage("Surname must be between 3 and 50 characters.");

        // Email is required and must be a valid email address
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        // Password is required and must be between 6 and 100 characters
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(6, 100).WithMessage("Password must be between 6 and 100 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\@\!\?\*\.]").WithMessage("Password must contain at least one special character (e.g. @, !, ?, *).");
    }
}