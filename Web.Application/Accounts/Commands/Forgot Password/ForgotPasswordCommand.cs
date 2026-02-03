namespace Web.Application.Accounts.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email)
     : IRequest<ErrorOr<string>>;
}
