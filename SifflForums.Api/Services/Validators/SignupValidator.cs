using FluentValidation;
using SifflForums.Api.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Services.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpValidator()
        {
            RuleFor(signUp => signUp.Username)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(3)
                .Matches(@"^[a-zA-Z][\w]*$");

            RuleFor(signUp => signUp.Email).EmailAddress(); 
        }
    }
}
