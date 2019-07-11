using FluentValidation;
using SifflForums.Api.Models.Auth;
using SifflForums.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Services.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpViewModel>
    {
        SifflContext _dbContext; 

        public SignUpValidator(SifflContext dbContext)
        {
            this._dbContext = dbContext; 

            RuleFor(signUp => signUp.Username)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(3)
                .Matches(@"^[a-zA-Z][\w]*$");

            RuleFor(signUp => signUp.Email).EmailAddress();

            RuleFor(signUp => signUp.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Must(NotBeBlacklisted).WithMessage("That password has been blacklisted, please choose a different one.");
        }

        private bool NotBeBlacklisted(string arg)
        {
            return !_dbContext.BlacklistedPasswords.Any(o => o.Password == arg);
        }
    }
}
