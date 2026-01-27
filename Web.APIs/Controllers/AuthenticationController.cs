using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Accounts.AccountDTO;
using Web.Application.Accounts.Commands.CreateAccount;
using Web.Application.Accounts.Commands.ResendConfirmationEmail;
using Web.Application.Accounts.Commands.ConfirmEmail;
using Web.Application.Accounts.Commands.ForgotPassword;
using Web.Application.Accounts.Commands.VerifyForgotPasswordOtp;
using Web.Application.Accounts.Commands.ResetPassword;
using Web.Application.Accounts.Commands.Login;
using Web.Application.Accounts.Commands.RefreshToken;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        /// <summary>
        /// Register a new user using email and password.
        /// </summary>
        /// <remarks>
        /// Creates a new account and sends a confirmation OTP to the user's email.
        /// </remarks>
        /// <response code="200">User registered successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="409">Email already exists</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var command = new CreateAccountCommand(
                request.Email,
                request.Password
            );

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => ToProblem(errors)
            );
        }

        /// <summary>
        /// Resend email confirmation OTP.
        /// </summary>
        /// <remarks>
        /// Sends a new confirmation OTP if the email is not confirmed yet.
        /// </remarks>
        /// <response code="200">OTP sent successfully</response>
        /// <response code="409">Email already confirmed</response>
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail(
            [FromBody] ResendConfirmEmailRequest request)
        {
            var command = new ResendConfirmationEmailCommand(request.Email);

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => ToProblem(errors)
            );
        }

        /// <summary>
        /// Confirm user email using OTP.
        /// </summary>
        /// <response code="200">Email confirmed successfully</response>
        /// <response code="400">Invalid or expired OTP</response>
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(
            [FromBody] ConfirmEmailRequest request)
        {
            var command = new ConfirmEmailCommand(
                request.email,
                request.OTP
            );

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => ToProblem(errors)
            );
        }

        /// <summary>
        /// Login using email and password.
        /// </summary>
        /// <response code="200">JWT and Refresh Token returned</response>
        /// <response code="400">Invalid credentials</response>
        /// <response code="403">Email not confirmed or user locked</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var command = new LoginCommand(
                request.Email,
                request.Password
            );

            var result = await _mediator.Send(command);

            return result.Match(
                token => Ok(token),
                errors => ToProblem(errors)
            );
        }

        /// <summary>
        /// Send OTP to reset password.
        /// </summary>
        /// <remarks>
        /// An OTP will be sent to the registered email address.
        /// </remarks>
        /// <response code="200" >OTP sent successfully</response>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            [FromBody] ForgetPasswordDto request)
        {
            var command = new ForgotPasswordCommand(request.Email);

            var result = await _mediator.Send(command);

            return result.Match(
                message => Ok(new { message }),
                errors => ToProblem(errors)
            );
        }

        /// <summary>
        /// Verify OTP for forgot password.
        /// </summary>
        /// <remarks>
        /// Returns a password reset token if OTP is valid.
        /// </remarks>
        /// <response code="200">OTP verified successfully</response>
        /// <response code="400">Invalid or expired OTP</response>
        [HttpPost("verify-forgot-password-otp")]
        public async Task<IActionResult> VerifyForgotPasswordOtp(
            [FromBody] VerfiyCodeDto request)
        {
            var command = new VerifyForgotPasswordOtpCommand(
                request.Email,
                request.CodeOTP
            );

            var result = await _mediator.Send(command);

            return result.Match(
                token => Ok(new { resetToken = token }),
                errors => ToProblem(errors)
            );
        }

        /// <summary>
        /// Reset user password.
        /// </summary>
        /// <remarks>
        /// Requires a valid reset token returned from OTP verification.
        /// </remarks>
        /// <response code="200">Password reset successfully</response>
        /// <response code="400">Invalid token or password mismatch</response>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordDto request)
        {
            var command = new ResetPasswordCommand(
                request.Email,
                request.Token,
                request.NewPassword,
                request.ConfirmNewPassword
            );

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => ToProblem(errors)
            );
        }

        /// <summary>
        /// Refresh JWT token using refresh token.
        /// </summary>
        /// <remarks>
        /// Returns a new JWT and refresh token.
        /// </remarks>
        /// <response code="200">New tokens generated</response>
        /// <response code="400">Invalid token or refresh token</response>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand(
                request.Token,
                request.RefreshToken
            );

            var result = await _mediator.Send(command);

            return result.Match(
                token => Ok(token),
                errors => ToProblem(errors)
            );
        }
    }
}
