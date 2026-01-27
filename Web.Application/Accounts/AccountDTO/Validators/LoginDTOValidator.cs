using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Accounts.AccountDTO;

namespace Web.Application.Accounts.AccountDTO.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }

    }
}
