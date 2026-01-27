using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Accounts.Commands.CreateAccount;

namespace Web.Application.Accounts.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .EmailAddress();

            RuleFor(x => x.OTP)
              .NotEmpty().WithMessage("OTP is required")
              .Matches(@"^\d{6}$")
              .WithMessage("OTP must be exactly 6 digits");


        }
    }
}
