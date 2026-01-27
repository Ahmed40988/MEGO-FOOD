using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.Users.Persistence
{
    public class UserRepository(AppDbContext dbContext ) : IUserRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> ExistsAsync(string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email==Email, cancellationToken);
        }
    }
}
