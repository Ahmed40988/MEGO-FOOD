using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Accounts.Commands.ResendConfirmationEmail
{
        public record ResendConfirmationEmailCommand(string Email)
            : IRequest<ErrorOr<Success>>;
    }


