using Web.Application.Common.Interfaces;
using Web.Domain.MenuCategories;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.MenuCategories.Persistence
{
    public class MenuCategoriesRepository(
        AppDbContext dbContext,
        IFuzzySearchRepository fuzzySearchRepository)
        : IMenuCategoryRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IFuzzySearchRepository _fuzzySearchRepository = fuzzySearchRepository;

        public async Task AddAsync(MenuCategory menuCategory, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(menuCategory, cancellationToken);
        }

        public Task DeleteAsync(MenuCategory menuCategory, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(menuCategory);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid CategoryId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuCategories
                .AsNoTracking()
                .AnyAsync(c => c.Id == CategoryId && !c.Deleted, cancellationToken);
        }

        public async Task<MenuCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuCategories
                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted, cancellationToken);
        }
        public async Task<MenuCategory?> GetByIdIncludeProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuCategories.Include(m=>m.Products)
                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted, cancellationToken);
        }
        public async Task<MenuCategory?> GetByNameAsync(string Name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuCategories
                .FirstOrDefaultAsync(c => c.Name == Name && !c.Deleted, cancellationToken);
        }

        public async Task<IReadOnlyList<MenuCategory>> GetByRestaurantIdAsync(
            Guid RestaurantId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuCategories
                .AsNoTracking()
                .Where(c => c.RestaurantId == RestaurantId && !c.Deleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<MenuCategory>> ListAsync()
        {
            return await _dbContext.MenuCategories
                .AsNoTracking()
                .Where(c => !c.Deleted)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(List<MenuCategory> menuCategories)
        {
            _dbContext.RemoveRange(menuCategories);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<MenuCategory>> SearchAsync(
            string keyword,
            CancellationToken cancellationToken = default)
        {
            var categories = await _dbContext.MenuCategories
                .AsNoTracking()
                .Where(c => !c.Deleted)
                .ToListAsync(cancellationToken);

            var result = categories
                .Select(c => new
                {
                    Category = c,
                    Score = _fuzzySearchRepository.CalculateSimilarity(keyword, c.Name)
                })
                .Where(x => x.Score >= 80)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Category)
                .ToList();

            return result;
        }

        public Task UpdateAsync(MenuCategory menuCategory, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(menuCategory);
            return Task.CompletedTask;
        }
    }
}
