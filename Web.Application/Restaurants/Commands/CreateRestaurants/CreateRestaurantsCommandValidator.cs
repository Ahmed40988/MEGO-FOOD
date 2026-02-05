namespace Web.Application.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantsCommandValidator : AbstractValidator<CreateRestaurantsCommand>
    {
        public CreateRestaurantsCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15);

            RuleFor(x => x.Description)
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.BaseCatgoryId)
                .NotEmpty();

        }

    }
}
