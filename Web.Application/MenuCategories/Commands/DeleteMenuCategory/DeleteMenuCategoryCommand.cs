namespace Web.Application.MenuCategories.Commands.DeleteMenuCategory
{
    public record DeleteMenuCategoryCommand(string AdminId,Guid CategoryId) : IRequest<ErrorOr<Deleted>>;
}
