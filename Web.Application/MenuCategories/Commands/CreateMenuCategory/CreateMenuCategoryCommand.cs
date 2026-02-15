using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Commands.CreateMenuCategory
{
    public record CreateMenuCategoryCommand(
          string Name,
          string Description,
          Guid Restaurantid) : IRequest<ErrorOr<Guid>>;
}
