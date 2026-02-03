namespace Web.Application.BaseCategories.Commands.CreateBaseCategory;

public class CreateBaseCategoryCommandValidator : AbstractValidator<CreateBaseCategoryCommand>
{
    public CreateBaseCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.UserId).NotEmpty();
    }
}
