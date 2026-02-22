using System.Linq.Dynamic.Core;
using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.Common;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Pagination;
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

        public async Task<PaginatedList<BaseCategoryResponse>> GetAllAsync(
            RequestFilters filters,
            CancellationToken cancellationToken = default)
        {
            var query = _dbContext.BaseCategories
                .Where(b => !b.Deleted)
                .AsNoTracking()
                .AsQueryable();

       
            if (!string.IsNullOrEmpty(filters.SearchValue))
            {
                query = query.Where(b =>
                    b.Name.Contains(filters.SearchValue) ||
                    b.Description.Contains(filters.SearchValue));
            }

            
            var totalCount = await query.CountAsync(cancellationToken);

         
            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            else
            {
                query = query.OrderBy(x => x.Name);
            }

            if (filters.PageNumber > 0 && filters.PageSize > 0)
            {
                var skip = (filters.PageNumber - 1) * filters.PageSize;
                query = query.Skip(skip).Take(filters.PageSize);
            }

            var items = await query
                .Select(x => new BaseCategoryResponse(
                    x.Id,
                    x.Name,
                    x.Description))
                .ToListAsync(cancellationToken);

            return new PaginatedList<BaseCategoryResponse>
                (items,
                filters.PageNumber, 
                totalCount, 
                filters.PageSize);
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

