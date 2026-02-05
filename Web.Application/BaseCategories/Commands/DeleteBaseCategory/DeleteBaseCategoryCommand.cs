namespace Web.Application.BaseCategories.Commands.DeleteBaseCategory;

public record DeleteBaseCategoryCommand(Guid Id, string AdminId)
    : IRequest<ErrorOr<Deleted>>;
