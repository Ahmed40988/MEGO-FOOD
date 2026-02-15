using Web.Application.MenuCategories.MenuCategoryDTO;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.listMenuCategory
{
    public class listMenuCategoriesQueryHandler(IRestaurantRepository restaurantCategoryRepository, IMenuCategoryRepository menuCategoryRepository) : IRequestHandler<listMenuCategoriesQuery, ErrorOr<List<MenuCategoryResponse>>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantCategoryRepository;
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;

        public async Task<ErrorOr<List<MenuCategoryResponse>>> Handle(listMenuCategoriesQuery query, CancellationToken cancellationToken)
        {

            var listMenuCategories = await _menuCategoryRepository.ListAsync();

            if (listMenuCategories != null)
            {

                var list = new List<MenuCategoryResponse>();
                MenuCategoryResponse item = null;
                foreach (var category in listMenuCategories)
                {
                    item = new MenuCategoryResponse(category.Id, category.Name, category.Description, category.RestaurantId);

                    list.Add(item);
                }
                return list;
            }
            else
                return Error.NotFound("MenuCategory.NotFound", "No MenuCategory found.");
        }
    }
}
