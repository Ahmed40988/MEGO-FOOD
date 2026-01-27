using Microsoft.EntityFrameworkCore;
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
                .AnyAsync(c => c.Id == CategoryId, cancellationToken);
        }

        public async Task<MenuCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuCategories
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<MenuCategory>> GetByRestaurantCategoryAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuCategories
                .AsNoTracking()
                .Where(c => c.RestaurantCategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<MenuCategory>> ListByrestaurantCategoryIdAsync(Guid restaurantCategoryId)
        {
            return await _dbContext.MenuCategories
                .AsNoTracking()
                .Where(c => c.RestaurantCategoryId == restaurantCategoryId)
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
