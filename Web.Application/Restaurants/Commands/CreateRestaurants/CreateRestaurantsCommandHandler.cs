
namespace Web.Application.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantsCommandHandler
        (IRestaurantRepository restaurantCategoryRepository,
        IUnitOfWork unitOfWork,IUserRepository userRepository)
        : IRequestHandler<CreateRestaurantsCommand,ErrorOr<Restaurant>>
    {
        private readonly IRestaurantRepository _restaurantCategoryRepository = restaurantCategoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<Restaurant>> Handle(CreateRestaurantsCommand command, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.ExistsAsync(command.userId, cancellationToken);

            if (!userExists)
            {
                return Error.Validation(
                    code: "User.NotFound",
                    description: "User does not exist");
            }

            var restaurant = new Restaurant
                (
                command.name,
                command.description,
                command.userId);



            await _restaurantCategoryRepository.AddAsync (restaurant);
            await _unitOfWork.CommitChangesAsync();
            return restaurant;
        }
    }
}
