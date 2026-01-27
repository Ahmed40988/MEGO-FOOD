using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Accounts.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email)
     : IRequest<ErrorOr<string>>;
}
