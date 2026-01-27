using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.listMenuCategory
{
    public class listMenuCategoriesQueryHandler(IRestaurantRepository restaurantCategoryRepository, IMenuCategoryRepository menuCategoryRepository): IRequestHandler<listMenuCategoriesQuery, ErrorOr<List<MenuCategory>>>
    {
        private readonly IRestaurantRepository _restaurantCategoryRepository = restaurantCategoryRepository;
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;

        public async Task<ErrorOr<List<MenuCategory>>> Handle(listMenuCategoriesQuery query, CancellationToken cancellationToken)
        {

            if (!await _restaurantCategoryRepository.ExistsAsync(query.RestaurantCatgoryId))
            {
                return Error.NotFound(description: "Restaurant Category not found");
            }

            return await _menuCategoryRepository.ListByrestaurantCategoryIdAsync(query.RestaurantCatgoryId);
        }
    }
}
