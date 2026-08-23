using System.Security.Cryptography;
using Web.Application.Accounts.AccountDTO;
using Web.Domain.Users;
using RefreshTokenEntity = Web.Domain.RefreshTokens.RefreshToken;


namespace Web.Application.Accounts.Commands.Login
{
    public class LoginCommandHandler(
        UserManager<AppUser> userManager,
        ITokenService tokenService
    ) : IRequestHandler<LoginCommand, ErrorOr<TokenDTO>>
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<ErrorOr<TokenDTO>> Handle(
            LoginCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                return Error.Validation("Auth.InvalidCredentials", "Invalid email or password");


            var isPasswordValid =
                await _userManager.CheckPasswordAsync(
                    user, command.Password);

            if (!isPasswordValid)
                return Error.Validation(
                    "Auth.InvalidCredentials",
                    "Invalid email or password");

            if (await _userManager.IsLockedOutAsync(user))
                return Error.Forbidden(
                    "Auth.Locked",
                    "User is locked");

            var jwt = await _tokenService.GenerateTokenAsync(user, userManager);
            var refreshTokenValue = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            var refreshToken = RefreshTokenEntity.Create(
                refreshTokenValue,
                refreshTokenExpiration
            );

            user.AddrefreshTokens(refreshToken);

            await _userManager.UpdateAsync(user);


            var isProfileComplete  = user.IsProfileCompleted();

            return new TokenDTO
            {
                UserId = user.Id,
                Token = jwt.Token,
                expiresIn = jwt.expiresIn,
                RefreshToken = refreshTokenValue,
                RefreshTokenExpiration = refreshTokenExpiration,
                IsCompleteProfile = isProfileComplete

            };

        }
    }
}
