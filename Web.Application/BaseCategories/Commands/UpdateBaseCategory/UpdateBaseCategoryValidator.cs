namespace Web.Application.BaseCategories.Commands.UpdateBaseCategory;

public class UpdateBaseCategoryValidator : AbstractValidator<UpdateBaseCategoryCommand>
{
    public UpdateBaseCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.AdminId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
