namespace Web.Application.MenuCategories.Commands.UpdateMenuCategory;

public class UpdateMenuCategoryValidator : AbstractValidator<UpdateMenuCategoryCommand>
{
    public UpdateMenuCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.AdminId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
