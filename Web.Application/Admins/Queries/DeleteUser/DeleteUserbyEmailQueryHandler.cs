using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Users;

namespace Web.Application.Admins.Queries.DeleteUser
{
    public class DeleteUserbyEmailQueryHandler(UserManager<AppUser> userManager,IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserbyEmailQuery, ErrorOr<Deleted>>
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteUserbyEmailQuery Query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(Query.Email);
            if (user == null) 
                return Error.NotFound();

            user.Delete(Query.Adminid);
            await _userManager.UpdateAsync(user);   
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }
    }
}
