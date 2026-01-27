using ErrorOr;
using MediatR;

namespace Web.Application.Accounts.Commands.VerifyForgotPasswordOtp
{
    public record VerifyForgotPasswordOtpCommand(string Email, string OTP)
        : IRequest<ErrorOr<string>>;
}
