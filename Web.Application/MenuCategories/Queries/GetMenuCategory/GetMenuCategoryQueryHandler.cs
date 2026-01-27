using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.GetMenuCategory
{
    public class GetMenuCategoryQueryHandler(IRestaurantRepository restaurantCategoryRepository,IMenuCategoryRepository menuCategoryRepository) : IRequestHandler<GetMenuCategoryQuery, ErrorOr<MenuCategory>>
    {
        private readonly IRestaurantRepository _restaurantCategoryRepository = restaurantCategoryRepository;
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;

        public async Task<ErrorOr<MenuCategory>> Handle(GetMenuCategoryQuery comand, CancellationToken cancellationToken)
        {

            if (!await _restaurantCategoryRepository.ExistsAsync(comand.RestaurantCategoryId, cancellationToken))
            {
                return Error.NotFound("Restaurant Category  not found");
            }

            if (await _menuCategoryRepository.GetByIdAsync(comand.menuCategoryId,cancellationToken) is not MenuCategory menuCategory)
            {
                return Error.NotFound(description: "menu Category not found");
            }

            return menuCategory;
        }
    }
}
