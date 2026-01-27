using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Accounts.AccountDTO;
using Web.Application.Common.Interfaces;
using Web.Domain.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandHandler(IUserRepository userRepository
        , IUnitOfWork unitOfWork
        ,UserManager<AppUser> userManager
        , ITokenService tokenService
        ,IMemoryCache memoryCache
        ,IEmailService emailService) : IRequestHandler<CreateAccountCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly IEmailService _emailService = emailService;

        public async Task<ErrorOr<Success>> Handle(CreateAccountCommand Command, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.ExistsByEmailAsync(Command.Email, cancellationToken);

            if (userExists)
            {
                return Error.Conflict(
                    code: "User.Dublicated",
                    description: "Email is already exist");
            }

            var user = new AppUser(Command.Email);
            var result=await _userManager.CreateAsync(user,Command.Passsword);

            if (result.Succeeded)
            {
                var otp = new Random().Next(100000, 999999).ToString();
                _memoryCache.Set($"EmailOTP_{Command.Email}", otp, TimeSpan.FromMinutes(5));
                await _emailService.SendConfirmationEmail(user, otp);
                return Result.Success;
            }

            return result.Errors
         .Select(e => Error.Validation(
             code: $"Identity.{e.Code}",
             description: e.Description
         )).FirstOrDefault();

        }
    }
}
