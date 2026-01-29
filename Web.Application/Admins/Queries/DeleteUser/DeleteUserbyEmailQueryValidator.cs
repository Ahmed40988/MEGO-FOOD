using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Admins.Queries.DeleteUser
{
    public class DeleteUserbyEmailQueryValidator:AbstractValidator<DeleteUserbyEmailQuery>
    {
        public DeleteUserbyEmailQueryValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();


            

        }

    }
}
