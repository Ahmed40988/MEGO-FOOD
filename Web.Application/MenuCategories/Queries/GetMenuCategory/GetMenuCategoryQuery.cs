using Web.Application.MenuCategories.MenuCategoryDTO;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.GetMenuCategory
{
    public record GetMenuCategoryQuery(Guid RestaurantId, Guid menuCategoryId) : IRequest<ErrorOr<MenuCategoryResponse>>;
}
