using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Users;

namespace Web.Application.Accounts.AccountDTO
{
    public record UserProfileDto(
       string Id,
       string Email,
       string FullName,
       string? PhotoUrl,
       Gender Gender,
       DateOnly DateOfBirth,
       DateTime CreatedOn
   );
}
