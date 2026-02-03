namespace Web.Application.Restaurants.Queries.GetRestaurant
{
    public class GetRestaurantQueryHandler(IRestaurantRepository restaurantCategoryRepository) : IRequestHandler<GetRestaurantQuery, ErrorOr<Restaurant>>
    {
        private readonly IRestaurantRepository _restaurantCategoryRepository = restaurantCategoryRepository;

        public async Task<ErrorOr<Restaurant>> Handle(GetRestaurantQuery command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantCategoryRepository.GetByIdAsync(command.CategoryId);
            return restaurant is null ?
                Error.NotFound(description: "Restaurant by this Id is not found !")
                : restaurant;

        }
    }
}
