using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.GetMenuCategory
{
    public record GetMenuCategoryQuery(Guid RestaurantCategoryId, Guid menuCategoryId) : IRequest<ErrorOr<MenuCategory>>;
}
