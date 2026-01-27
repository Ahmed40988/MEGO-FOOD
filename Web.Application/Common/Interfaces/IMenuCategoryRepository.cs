using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.MenuCategories;

namespace Web.Application.Common.Interfaces
{
    public interface IMenuCategoryRepository
    {
        Task<MenuCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<MenuCategory>> GetByRestaurantCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<MenuCategory>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        Task<List<MenuCategory>> ListByrestaurantCategoryIdAsync(Guid restaurantCategoryId);
        Task<bool> ExistsAsync(Guid CategoryId, CancellationToken cancellationToken = default);

        Task AddAsync(MenuCategory menuCategory, CancellationToken cancellationToken = default);
        Task UpdateAsync(MenuCategory menuCategory, CancellationToken cancellationToken = default);
        Task DeleteAsync(MenuCategory menuCategory, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(List<MenuCategory> menuCategories);

    }
}
