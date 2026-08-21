using Web.Application.Restaurants.Contracts;

namespace Web.Application.Restaurants.Queries.GetRestaurant
{
    public class GetRestaurantQueryHandler(IRestaurantRepository restaurantCategoryRepository) : IRequestHandler<GetRestaurantQuery, ErrorOr<RestaurantResponce>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantCategoryRepository;

        public async Task<ErrorOr<RestaurantResponce>> Handle(GetRestaurantQuery command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(command.Id);

            return restaurant is null ?
                Error.NotFound("Restaurant.NotFound", "Restaurant by this Id is not found !")
                : new RestaurantResponce(restaurant.Id, restaurant.Name, restaurant.Description,restaurant.Rating, restaurant.BaseCatgoryId);

        }
    }
}
