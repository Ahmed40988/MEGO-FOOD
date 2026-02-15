namespace Web.Application.MenuCategories.Commands.UpdateMenuCategory;

public record UpdateMenuCategoryCommand(
    Guid Id,
    string Name,
    string Description,
    Guid  RestaurantId,
    string AdminId
) : IRequest<ErrorOr<Guid>>;

