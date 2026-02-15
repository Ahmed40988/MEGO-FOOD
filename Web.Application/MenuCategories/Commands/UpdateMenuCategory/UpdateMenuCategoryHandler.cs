
using Web.Application.Common.Interfaces;

namespace Web.Application.MenuCategories.Commands.UpdateMenuCategory;

public class UpdateMenuCategoryHandler(IUnitOfWork unitOfWork, IMenuCategoryRepository menuCategoryRepository,IRestaurantRepository restaurantRepository, IUserRepository userRepository) : IRequestHandler<UpdateMenuCategoryCommand, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<Guid>> Handle(UpdateMenuCategoryCommand command, CancellationToken cancellationToken)
    {
        var adminExists = await _userRepository.ExistsAsync(command.AdminId, cancellationToken);


        if (!adminExists)
        {
            return Error.Validation(
                code: "admin.NotFound",
                description: "admin does not exist");
        }

        var restaurant = await _restaurantRepository.GetByIdAsync(command.RestaurantId);

        if (restaurant == null)
            return Error.NotFound("restaurant.NotFound", "restaurant For this Id is not found");

        var entity = await _menuCategoryRepository.GetByIdAsync(command.Id);
        if (entity is null)
            return Error.NotFound("Menu Category.NotFound", "Menu Category With this Id is not Found");

        var exist = await _menuCategoryRepository.GetByNameAsync(command.Name, cancellationToken);

        if (exist is not null && exist.Id != command.Id)
        {
            return Error.Conflict(
                "MenuCategory.Duplicated",
                "Menu Category with the same name already exists"
            );
        }


        entity.Update(command.AdminId, command.Name, command.Description,command.RestaurantId);
        await _unitOfWork.CommitChangesAsync();
        return entity.Id;
    }
}
