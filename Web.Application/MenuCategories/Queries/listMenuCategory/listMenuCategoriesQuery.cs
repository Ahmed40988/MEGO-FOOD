using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.listMenuCategory
{
    public record listMenuCategoriesQuery(Guid RestaurantCatgoryId) : IRequest<ErrorOr<List<MenuCategory>>>;
}
