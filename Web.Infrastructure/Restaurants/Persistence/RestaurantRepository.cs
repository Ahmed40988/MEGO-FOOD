using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.Common;
using System.Linq.Dynamic.Core;

using Web.Application.Common.Interfaces;
using Web.Application.Common.Pagination;
using Web.Application.Restaurants.Contracts;
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

        public async Task<bool> ExistsAsync(Guid RestaurantId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants.AsNoTracking().AnyAsync(r => r.Id == RestaurantId && !r.Deleted);
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants.Include(r => r.MenuCategories)
        .FirstOrDefaultAsync(r => r.Id == id && !r.Deleted);
        }

        public async Task<Restaurant?> GetByNameAsync(string Name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants.FirstOrDefaultAsync(b => b.Name == Name && !b.Deleted, cancellationToken);
        }

        public async Task<PaginatedList<RestaurantResponce>> ListRestaurants(Guid ?baseCategoryId,
            RequestFilters filters,
            CancellationToken cancellationToken = default)
        {

            var query = _dbContext.Restaurants
                .Where(b => !b.Deleted)
                .AsNoTracking()
                .AsQueryable();

            if (baseCategoryId.HasValue)
            {
                query = query.Where(b => b.BaseCatgoryId == baseCategoryId.Value);
            }

            if (filters.TopRaing == true)
            {
                query = query.Where(b => b.Rating >= 4).OrderByDescending(x => x.Rating).Take(30);
            }
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
                .Select(x => new RestaurantResponce(
                    x.Id,
                    x.Name,
                    x.Description
                    ,x.Rating
                    ,x.BaseCatgoryId))
                .ToListAsync(cancellationToken);

            return new PaginatedList<RestaurantResponce>
                (items,
                filters.PageNumber,
                totalCount,
                filters.PageSize);

        }
        public Task RemoveRangeAsync(List<Restaurant> restaurantes)
        {
            _dbContext.RemoveRange(restaurantes);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Restaurant>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
        {

            var categories = await _dbContext.Restaurants
                .AsNoTracking()
                  .Where(r => !r.Deleted)
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


        public Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(restaurant);
            return Task.CompletedTask;
        }
    }
}
