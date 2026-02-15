using Microsoft.EntityFrameworkCore;
using Web.Domain.Users;

namespace Web.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByIdIncludeRestaurantAsync(string userId, CancellationToken cancellationToken = default);
            Task<bool> ExistsAsync(string userId, CancellationToken cancellationToken);
        Task<bool> ExistsByEmailAsync(string Email, CancellationToken cancellationToken);
        Task<bool> ExistSameEmailandDeletedAsync(string Email, CancellationToken cancellationToken);

        Task AddAsync(AppUser user, CancellationToken cancellationToken = default);
         Task DeleteAsync(AppUser user, CancellationToken cancellationToken = default);
        Task<AppUser?> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<AppUser?> GetByEmailAsync(string Email, CancellationToken cancellationToken = default);
        Task<List<AppUser>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<AppUser>> ListWithRestaurantAsync(CancellationToken cancellationToken = default);


          Task UpdateAsync(AppUser user, CancellationToken cancellationToken = default);
    }

}
