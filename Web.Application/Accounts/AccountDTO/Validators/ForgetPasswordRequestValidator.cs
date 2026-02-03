namespace Web.Application.Accounts.AccountDTO.Validators;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordDto>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}