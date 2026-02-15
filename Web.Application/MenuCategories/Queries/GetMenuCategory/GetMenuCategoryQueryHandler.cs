using Web.Application.MenuCategories.MenuCategoryDTO;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.GetMenuCategory
{
    public class GetMenuCategoryQueryHandler(IRestaurantRepository restaurantCategoryRepository, IMenuCategoryRepository menuCategoryRepository) : IRequestHandler<GetMenuCategoryQuery, ErrorOr<MenuCategoryResponse>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantCategoryRepository;
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;

        public async Task<ErrorOr<MenuCategoryResponse>> Handle(GetMenuCategoryQuery comand, CancellationToken cancellationToken)
        {

            if (!await _restaurantRepository.ExistsAsync(comand.RestaurantId, cancellationToken))
            {
                return Error.NotFound("Restaurant.NotFound", "Restaurant For This Menu is not found");
            }

            if (await _menuCategoryRepository.GetByIdAsync(comand.menuCategoryId, cancellationToken) is not MenuCategory menuCategory)
            {
                return Error.NotFound("menu Category.NotFound", "menu Category not found");
            }

            var response = new MenuCategoryResponse(menuCategory.Id,menuCategory.Name, menuCategory.Description, menuCategory.RestaurantId);

            return response;
        }
    }
}
