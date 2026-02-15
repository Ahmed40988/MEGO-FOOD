using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommandValidator:AbstractValidator<CreateMenuCategoryCommand>
    {
        public CreateMenuCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Restaurantid).NotEmpty();
        }
    }
}
