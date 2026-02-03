namespace Web.Application.BaseCategories.Commands.CreateBaseCategory;

public record CreateBaseCategoryCommand(
    string Name,
    string Description,
    string UserId
) : IRequest<ErrorOr<Guid>>;
