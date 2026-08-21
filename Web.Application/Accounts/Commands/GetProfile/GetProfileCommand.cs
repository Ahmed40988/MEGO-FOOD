using Web.Application.Accounts.AccountDTO;

namespace Web.Application.Accounts.Commands.GetProfile

{
    public record GetProfileCommand(string UserId) : IRequest<ErrorOr<UserProfileDto>>;
}
