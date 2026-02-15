using Web.Domain.MenuCategories;
using Web.Domain.Restaurants;

namespace Web.Application.MenuCategories.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommandHandler
      (IRestaurantRepository restaurantCategoryRepository,
      IMenuCategoryRepository menuCategoryRepository,
      IUnitOfWork unitOfWork)
      : IRequestHandler<CreateMenuCategoryCommand, ErrorOr<Guid>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantCategoryRepository;
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Guid>> Handle(CreateMenuCategoryCommand command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(command.Restaurantid);

            if (restaurant == null)
                return Error.NotFound("restaurant.NotFound", "restaurant For this Id is not found");

            var category = new MenuCategory(
                command.Name,
                command.Description,
                command.Restaurantid);

            var addmenucategoryResult = restaurant.AddMenuCategory(category);

            if (addmenucategoryResult.IsError)
                return addmenucategoryResult.Errors;

            await _menuCategoryRepository.AddAsync(category, cancellationToken);
            await _unitOfWork.CommitChangesAsync();
            return category.Id;
        }
    }
}
