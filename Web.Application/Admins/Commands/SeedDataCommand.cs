using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Admins.Commands
{
    public record SeedDataCommand(string UserId) : IRequest<ErrorOr<Success>>;
}
