using Web.Application.Common.Interfaces;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.Users.Persistence
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> ExistsAsync(string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Id == userId && !x.Deleted, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email == Email && !x.Deleted, cancellationToken);
        }
        public async Task<bool> ExistSameEmailandDeletedAsync(string Email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email == Email && x.Deleted, cancellationToken);
        }
    }
}
