using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Restaurants;

namespace Web.Application.Common.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Restaurant>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Restaurant>>ListRestaurants( CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid CategorytId, CancellationToken cancellationToken = default);

        Task AddAsync(Restaurant restaurantCategory, CancellationToken cancellationToken = default);
        Task UpdateAsync(Restaurant restaurantCategory, CancellationToken cancellationToken = default);
        Task DeleteAsync(Restaurant restaurantCategory, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(List<Restaurant> Restaurants);
    }
}
