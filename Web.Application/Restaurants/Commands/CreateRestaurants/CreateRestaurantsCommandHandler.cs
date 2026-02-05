
using Web.Application.Common.Interfaces;
using Web.Application.Restaurants.Contracts;
using Web.Domain.Users;

namespace Web.Application.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantsCommandHandler
        (IRestaurantRepository restaurantCategoryRepository,
        IBaseCategoryRepository baseCategoryRepository,
        IUnitOfWork unitOfWork,IUserRepository userRepository)
        : IRequestHandler<CreateRestaurantsCommand, ErrorOr<RestaurantResponce>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantCategoryRepository;
        private readonly IBaseCategoryRepository _baseCategoryRepository = baseCategoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<RestaurantResponce>> Handle(CreateRestaurantsCommand command, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.ExistsAsync(command.UserId,cancellationToken);

            if (!userExists)
            {
                return Error.Validation(
                    code: "User.NotFound",
                    description: "User does not exist");
            }

            var Exist = await _restaurantRepository.GetByNameAsync(command.Name, cancellationToken);

            if (Exist is not null)
                return Error.Conflict("Restaurant.Dublicated", "Restaurant with the same name is Exist!");


            var basecategoryExist = await _baseCategoryRepository.ExistsAsync(command.BaseCatgoryId, cancellationToken);
            if(!basecategoryExist)
                return Error.Validation(
            code: "BaseCategory.NotFound",
            description: "BaseCategory Id does not exist");
        

        var restaurant = new Restaurant
                (
                command.Name,
                command.Description,
                command.UserId,
                command.BaseCatgoryId);

            var response = new RestaurantResponce(restaurant.Id, restaurant.Name, restaurant.Description, restaurant.BaseCatgoryId);

            await _restaurantRepository.AddAsync(restaurant, cancellationToken);
            await _unitOfWork.CommitChangesAsync();

            return response;
        }
    }
}
