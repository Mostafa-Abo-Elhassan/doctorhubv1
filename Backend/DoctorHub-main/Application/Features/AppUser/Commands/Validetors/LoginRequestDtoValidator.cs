﻿using Application.Features.AppUser.Commands.Models;
using FluentValidation;

namespace Application.Features.AppUser.Commands.Validetors
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            //    RuleFor(x => x.Email)
            //        .NotEmpty().WithMessage("Email is required")
            //        .EmailAddress().WithMessage("Invalid email format");

            //    RuleFor(x => x.Password)
            //        .NotEmpty().WithMessage("Password is required");
        }
    }
}
