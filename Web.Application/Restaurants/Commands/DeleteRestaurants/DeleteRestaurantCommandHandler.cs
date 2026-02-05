
using Web.Domain.Users;
namespace Web.Application.Restaurants.Commands.DeleteRestaurants
{
    public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
        UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteRestaurantCommand, ErrorOr<Deleted>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteRestaurantCommand command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository
                .GetByIdAsync(command.Id, cancellationToken);

            if (restaurant == null)
                return Error.NotFound(
                 "Restaurant.NotFound",
                 "Restaurant with the given id was not found");

            var user = await _userManager.FindByIdAsync(restaurant.AppUserId);

            if (user == null)
                return Error.Validation(
               code: "User.NotFound",
               description: "User does not exist");

            var deleteResult = user.DeleteRestaurant(command.Id);
            if (deleteResult.IsError)
             return deleteResult.Errors;

            restaurant.Delete(command.AdminId);

            await _userManager.UpdateAsync(user);
            await _restaurantRepository.UpdateAsync(restaurant);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}

