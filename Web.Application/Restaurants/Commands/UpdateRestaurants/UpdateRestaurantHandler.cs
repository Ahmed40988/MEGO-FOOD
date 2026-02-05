using Web.Domain.Restaurants;

namespace Web.Application.Restaurants.Commands.UpdateRestaurants;

public class UpdateRestaurantHandler(IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository, IUserRepository userRepository) : IRequestHandler<UpdateRestaurantCommand, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<Guid>> Handle(UpdateRestaurantCommand command, CancellationToken cancellationToken)
    {
        var adminExists = await _userRepository.ExistsAsync(command.AdminId, cancellationToken);


        if (!adminExists)
        {
            return Error.Validation(
                code: "admin.NotFound",
                description: "admin does not exist");
        }
        var entity = await _restaurantRepository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
            return Error.NotFound("Restaurant.NotFound", "Restaurant With this Id is not Found");

        var exist = await _restaurantRepository.GetByNameAsync(command.Name, cancellationToken);

        if (exist is not null && exist.Id != command.Id)
        {
            return Error.Conflict(
                "Restaurant.Duplicated",
                "Restaurant with the same name already exists"
            );
        }


        entity.Update(command.AdminId, command.Name, command.Description,command.BaseCatgoryId);
        await _unitOfWork.CommitChangesAsync();
        return entity.Id;
    }
}
