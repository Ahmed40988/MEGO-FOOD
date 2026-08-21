using Web.Application.Common.File;
using Web.Domain.Addresses;
using Web.Domain.Users;

namespace Web.Application.Accounts.Commands.CompleteProfile
{
    public class CompleteProfileCommandHandler(IFileStorageService fileStorageService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<CompleteProfileCommand, ErrorOr<Success>>
    {
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Success>> Handle(CompleteProfileCommand command, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user is null)
                return Error.NotFound("User.NotFound", "User not found");

            user.UpdateProfile(
                command.FullName,
                command.Phone,
                command.DateOfBirth
            );
    
            if (command.Image is not null)
            {
                using var stream = command.Image.OpenReadStream();
                user.PhotoURl = await _fileStorageService.SaveFileAsync(stream, command.Image.FileName, "Users-images");
            }

            if (string.IsNullOrWhiteSpace(user.PhotoURl))
                return Error.Validation("ImageUploadFailed", "Failed to upload image");


            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return Error.Failure("User.UpdateFailed", "Failed to update user profile");

            await _unitOfWork.CommitChangesAsync();

            return Result.Success;


        }
    }
}
