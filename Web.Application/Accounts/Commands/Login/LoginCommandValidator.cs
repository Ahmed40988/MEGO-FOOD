namespace Web.Application.Accounts.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();


            RuleFor(x => x.Password)
    .NotEmpty()
    .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
    .MaximumLength(12).WithMessage("Password must not exceed 12 characters")
    .Matches(PasswordRegexPatterns.Password).WithMessage("Password must contain uppercase and lowercase letters, numbers, and special characters");

        }
    }
}
