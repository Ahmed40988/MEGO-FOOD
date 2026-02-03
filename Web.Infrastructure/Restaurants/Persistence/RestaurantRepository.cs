using Web.Application.Common.Interfaces;
using Web.Domain.Restaurants;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.Restaurants.Persistence
{
    public class RestaurantRepository(AppDbContext dbContext, IFuzzySearchRepository fuzzySearchRepository) : IRestaurantRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IFuzzySearchRepository _fuzzySearchRepository = fuzzySearchRepository;

        public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(restaurant, cancellationToken);
        }

        public Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(restaurant);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid CategorytId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants.AsNoTracking().AnyAsync(c => c.Id == CategorytId);
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyList<Restaurant>> ListRestaurants(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }


        public Task RemoveRangeAsync(List<Restaurant> restaurantCategories)
        {
            _dbContext.RemoveRange(restaurantCategories);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Restaurant>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
        {

            var categories = await _dbContext.Restaurants
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


        public Task UpdateAsync(Restaurant restaurantCategory, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(restaurantCategory);
            return Task.CompletedTask;
        }
    }
}
