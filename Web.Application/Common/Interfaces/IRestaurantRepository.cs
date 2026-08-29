using Web.Application.Common.Pagination;
using Web.Application.Restaurants.Contracts;

namespace Web.Application.Common.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Restaurant?> GetByNameAsync(string Name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Restaurant>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        Task<PaginatedList<RestaurantResponce>> ListRestaurants(Guid? baseCategoryId, RestaurantFilters filters,CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid CategorytId, CancellationToken cancellationToken = default);

        Task AddAsync(Restaurant restaurantCategory, CancellationToken cancellationToken = default);
        Task UpdateAsync(Restaurant restaurantCategory, CancellationToken cancellationToken = default);
        Task DeleteAsync(Restaurant restaurantCategory, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(List<Restaurant> Restaurants);
    }
}
