namespace Web.Application.BaseCategories.Commands.UpdateBaseCategory;

public record UpdateBaseCategoryCommand(
    Guid Id,
    string Name,
    string Description,
    string AdminId
) : IRequest<ErrorOr<Guid>>;

