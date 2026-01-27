using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Web.Application.Common.Interfaces;
using Web.Domain.RefreshTokens;
using Web.Domain.Users;
using Web.Application.Accounts.AccountDTO;
using System.Security.Cryptography;
using RefreshTokenEntity = Web.Domain.RefreshTokens.RefreshToken;


namespace Web.Application.Accounts.Commands.Login
{
    public class LoginCommandHandler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService
    ) : IRequestHandler<LoginCommand, ErrorOr<TokenDTO>>
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<ErrorOr<TokenDTO>> Handle(
            LoginCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                return Error.Validation("Auth.InvalidCredentials", "Invalid email or password");

            var result = await _signInManager.PasswordSignInAsync(
                user, command.Password, false, true);

            if ( user.EmailConfirmed != true)
                return Error.Forbidden("Auth.EmailNotConfirmed", "Email not confirmed");

            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                    return Error.Forbidden("Auth.EmailNotConfirmed", "Email not confirmed");

                if (result.IsLockedOut)
                    return Error.Forbidden("Auth.Locked", "User is locked");

                return Error.Validation("Auth.InvalidCredentials", "Invalid email or password");
            }

            var jwt = await _tokenService.GenerateTokenAsync(user, userManager);
            var refreshTokenValue = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            var refreshToken = RefreshTokenEntity.Create(
                refreshTokenValue,
                refreshTokenExpiration
            );

            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);

            return new TokenDTO
            {
                UserId = user.Id,
                Token = jwt.Token,
                expiresIn = jwt.expiresIn,
                RefreshToken = refreshTokenValue,
                RefreshTokenExpiration = refreshTokenExpiration
            };

        }
    }
}
