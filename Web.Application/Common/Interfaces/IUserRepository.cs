namespace Web.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string userId, CancellationToken cancellationToken);
        Task<bool> ExistsByEmailAsync(string Email, CancellationToken cancellationToken);
        Task<bool> ExistSameEmailandDeletedAsync(string Email, CancellationToken cancellationToken);
    }

}
