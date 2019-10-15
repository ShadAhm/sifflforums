using FluentValidation;
using SifflForums.Data;
using SifflForums.Service.Models.Dto;
using System.Linq;

namespace SifflForums.Service.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpModel>
    {
        SifflContext _dbContext;

        public SignUpValidator(SifflContext dbContext)
        {
            this._dbContext = dbContext;

            RuleFor(signUp => signUp.Username)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(3)
                .Matches(@"^[a-zA-Z][\w]*$")
                .Must(BeUnique).WithMessage("That username already exists.");

            RuleFor(signUp => signUp.Email).EmailAddress();

            RuleFor(signUp => signUp.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Must(NotBeBlacklisted).WithMessage("That password has been blacklisted, please choose a different one.");
        }

        private bool BeUnique(string arg)
        {
            return !_dbContext.Users.Any(u => u.Username == arg);
        }

        private bool NotBeBlacklisted(string arg)
        {
            return !_dbContext.BlacklistedPasswords.Any(bp => bp.Password == arg);
        }
    }
}
