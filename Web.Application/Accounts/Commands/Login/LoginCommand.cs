using Web.Application.Accounts.AccountDTO;

namespace Web.Application.Accounts.Commands.Login
{
    public record LoginCommand(string Email, string Password)
        : IRequest<ErrorOr<TokenDTO>>;
}
