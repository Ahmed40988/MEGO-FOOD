namespace Web.Application.Restaurants.Queries.ListRestaurant
{
    public class ListRestaurantQueryHandler(IRestaurantRepository restaurantCategoryRepository) : IRequestHandler<ListRestaurantQuerys, ErrorOr<List<Restaurant>>>
    {
        private readonly IRestaurantRepository _restaurantCategoryRepository = restaurantCategoryRepository;

        public async Task<ErrorOr<List<Restaurant>>> Handle(ListRestaurantQuerys command, CancellationToken cancellationToken)
        {
            var listcategories = await _restaurantCategoryRepository.ListRestaurants();

            return listcategories is null ?
                 Error.NotFound(description: "Restaurants is  Empty !")
                 : listcategories.ToList();

        }
    }
}
