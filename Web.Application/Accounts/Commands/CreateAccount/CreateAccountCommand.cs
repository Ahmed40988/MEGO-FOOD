using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Accounts.Commands.CreateAccount
{
    public record CreateAccountCommand(string Email,string Passsword):IRequest<ErrorOr<Success>>;
}
