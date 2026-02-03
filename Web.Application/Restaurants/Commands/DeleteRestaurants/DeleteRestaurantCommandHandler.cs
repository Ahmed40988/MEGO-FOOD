
using Web.Domain.Users;
namespace Web.Application.Restaurants.Commands.DeleteRestaurants
{
    public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantCategoryRepository,
        UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteRestaurantCommand, ErrorOr<Deleted>>
    {
        private readonly IRestaurantRepository _restaurantCategoryRepository = restaurantCategoryRepository;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteRestaurantCommand command, CancellationToken cancellationToken)
        {
            var RestaurantCategoy = await _restaurantCategoryRepository
                .GetByIdAsync(command.CategoryId, cancellationToken);

            if (RestaurantCategoy == null)
                return Error.NotFound(description: "restaurant is Already Deleted!");

            var user = await _userManager.FindByIdAsync(RestaurantCategoy.userid);

            if (user == null)
                return Error.Unexpected(description: "User is not found for this restaurant ");

            var result = user.DeleteRestaurant(command.CategoryId);
            if (result.IsError)
                return result.Errors;

            RestaurantCategoy.Delete("Admin ID"); //TODO
            await _userManager.UpdateAsync(user);
            await _restaurantCategoryRepository.UpdateAsync(RestaurantCategoy);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}

