using ErrorOr;
using MediatR;
using Web.Application.Accounts.AccountDTO;

namespace Web.Application.Accounts.Commands.RefreshToken
{
    public record RefreshTokenCommand(string Token, string RefreshToken)
        : IRequest<ErrorOr<TokenDTO>>;
}
