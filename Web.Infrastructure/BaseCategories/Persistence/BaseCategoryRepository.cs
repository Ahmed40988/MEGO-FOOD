using System.Linq.Dynamic.Core;
using Web.Application.Common.Interfaces;
using Web.Domain.BaseCategories;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.BaseCategories.Persistence
{
    public class BaseCategoryRepository(AppDbContext dbContext)
        : IBaseCategoryRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task AddAsync(
            BaseCategory entity,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
        }

        public async Task<BaseCategory?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.BaseCategories
                .FirstOrDefaultAsync(b => b.Id == id&&!b.Deleted, cancellationToken);
        }

        public async Task<BaseCategory?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.BaseCategories
                .FirstOrDefaultAsync(b => b.Name == name && !b.Deleted, cancellationToken);
        }

        public async Task<IReadOnlyList<BaseCategory>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.BaseCategories.Where(b=>!b.Deleted)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(
            BaseCategory entity,
            CancellationToken cancellationToken = default)
        {
            _dbContext.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid CategorytId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.BaseCategories.AsNoTracking().AnyAsync(c => c.Id == CategorytId&&!c.Deleted);
        }
    }
}

