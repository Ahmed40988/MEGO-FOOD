using Web.Application.Accounts.AccountDTO;
using Web.Application.Common.File;
using Web.Domain.Users;

namespace Web.Application.Accounts.Commands.GetProfile
{
    public class GetProfileCommandHandler( UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        : IRequestHandler<GetProfileCommand, ErrorOr<UserProfileDto>>
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async  Task<ErrorOr<UserProfileDto>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                return Error.NotFound("User.NotFound", "User with the specified ID was not found.");

            return new UserProfileDto(
                user.Id,
                user.Email!,
                user.FullName,
                user.PhotoURl,
                user.gender,
                user.DateOfBirth,
                user.Createdon
            );
        }
    }
    }

