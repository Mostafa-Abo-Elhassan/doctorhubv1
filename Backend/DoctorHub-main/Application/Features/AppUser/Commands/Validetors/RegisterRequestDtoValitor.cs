using Application.Features.AppUser.Commands.Models;
using FluentValidation;

namespace Application.Features.AppUser.Commands.Validetors
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        #region Constructors
        public RegisterRequestDtoValidator()
        {
            ApplyValidationRules();
        }
        #endregion

        #region Actions
        private void ApplyValidationRules()
        {
            //RuleFor(x => x.UserName)
            //    .NotEmpty().WithMessage("Name must not be empty")
            //    .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            //RuleFor(x => x.Email)
            //    .NotEmpty().WithMessage("Email must not be empty")
            //    .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            //    .EmailAddress().WithMessage("Invalid email format");

            //RuleFor(x => x.ProfilePhoto)
            //    .MaximumLength(255).WithMessage("Profile photo path must not exceed 255 characters")
            //    .When(x => x.ProfilePhoto != null); // Only validate if provided

            //RuleFor(x => x.PhoneNumber)
            //    .NotEmpty().WithMessage("Phone must not be empty")
            //    .Matches(@"^\+?1?\d{9,15}$").WithMessage("Phone number must be valid (9-15 digits)");

            //RuleFor(x => x.Password)
            //    .NotEmpty().WithMessage("Password must not be empty")
            //    .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            //    .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            //    .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            //    .Matches(@"\d").WithMessage("Password must contain at least one number");

            //RuleFor(x => x.ConfirmPassword)
            //    .NotEmpty().WithMessage("Confirm password must not be empty")
            //    .Equal(x => x.Password).WithMessage("Passwords must match");

            //RuleFor(x => x.Role)
            //    .MaximumLength(50).WithMessage("Role must not exceed 50 characters")
            //    .When(x => x.Role != null); // Only validate if provided

            //RuleFor(x => x.PaymentInfo)
            //    .MaximumLength(255).WithMessage("Payment info must not exceed 255 characters")
            //    .When(x => x.PaymentInfo != null); // Only validate if provided
        }
        #endregion
    }
}