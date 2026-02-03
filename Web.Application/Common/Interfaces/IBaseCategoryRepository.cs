using Web.Domain.BaseCategories;

namespace Web.Application.Common.Interfaces
{
    public interface IBaseCategoryRepository
    {
        Task<BaseCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BaseCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<BaseCategory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(BaseCategory entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(BaseCategory entity, CancellationToken cancellationToken = default);
    }

}
