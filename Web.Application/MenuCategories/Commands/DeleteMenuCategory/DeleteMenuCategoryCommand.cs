namespace Web.Application.MenuCategories.Commands.DeleteMenuCategory
{
    public record DeleteMenuCategoryCommand(Guid CategoryId) : IRequest<ErrorOr<Deleted>>;
}
