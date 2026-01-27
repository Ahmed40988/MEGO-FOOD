using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Accounts.Commands.VerifyForgotPasswordOtp;

namespace Web.Application.Accounts.Commands.VerifyForgotPasswordOTP
{
    public class VerifyForgotPasswordOtpCommandValidator:AbstractValidator<VerifyForgotPasswordOtpCommand>
    {
        public VerifyForgotPasswordOtpCommandValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .EmailAddress();

            RuleFor(x => x.OTP)
            .NotEmpty().WithMessage("OTP is required")
            .Matches(@"^\d{6}$")
            .WithMessage("OTP must be exactly 6 digits");

        }
    }
}
