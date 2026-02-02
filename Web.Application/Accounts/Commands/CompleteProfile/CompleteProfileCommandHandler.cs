using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Adresses.AddressDTO;
using Web.Domain.Addresses;
using Web.Domain.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Application.Accounts.Commands.CompleteProfile
{
    public class CompleteProfileCommandHandler(IFileHelperService fileHelperService,UserManager<AppUser> userManager,IUnitOfWork unitOfWork) : IRequestHandler<CompleteProfileCommand, ErrorOr<Success>>
    {
        private readonly IFileHelperService _fileHelperService = fileHelperService;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Success>> Handle(CompleteProfileCommand command, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user is null)
                return Error.NotFound("User.NotFound", "User not found");

            var address = Address.Create(
                command.Address.Street,
                command.Address.City,
                command.Address.State,
                command.Address.PostalCode,
                command.Address.Country,
                command.UserId
            );

            user.UpdateProfile(
                command.FullName,
                command.Phone,
                command.DateOfBirth,
                address
            );

            if (command.Image is not null)
            {
                user.PhotoURl = _fileHelperService.UploadFile(command.Image, "User");
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return Error.Failure("User.UpdateFailed", "Failed to update user profile");

            await _unitOfWork.CommitChangesAsync();

            return Result.Success;


        }
    }
}
