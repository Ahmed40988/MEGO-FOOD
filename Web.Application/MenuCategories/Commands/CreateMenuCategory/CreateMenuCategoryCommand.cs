using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Commands.CreateMenuCategory
{
    public record CreateMenuCategoryCommand(
          string name,
          string description,
          Guid restaurantcategoryid) : IRequest<ErrorOr<MenuCategory>>;
}
