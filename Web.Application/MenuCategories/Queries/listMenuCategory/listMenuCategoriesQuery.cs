using Web.Application.MenuCategories.MenuCategoryDTO;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.listMenuCategory
{
    public record listMenuCategoriesQuery() : IRequest<ErrorOr<List<MenuCategoryResponse>>>;
}
