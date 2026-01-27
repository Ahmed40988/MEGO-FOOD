using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Accounts.Commands.CreateAccount;

namespace Web.Application.Accounts.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommandValidator : AbstractValidator<ResendConfirmationEmailCommand>
    {
        public ResendConfirmationEmailCommandValidator()
        {
            RuleFor(x => x.Email)
             .NotEmpty()
             .EmailAddress();

        }

    }
}
