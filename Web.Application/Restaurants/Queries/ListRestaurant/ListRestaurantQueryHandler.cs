using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.Restaurants.Contracts;

namespace Web.Application.Restaurants.Queries.ListRestaurant
{
    public class ListRestaurantQueryHandler(IRestaurantRepository restaurantCategoryRepository) : IRequestHandler<ListRestaurantQuerys, ErrorOr<List<RestaurantResponce>>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantCategoryRepository;

        public async Task<ErrorOr<List<RestaurantResponce>>> Handle(ListRestaurantQuerys command, CancellationToken cancellationToken)
        {
            var listrestaurant = await _restaurantRepository.ListRestaurants();

            if(listrestaurant == null)
            {
                return Error.NotFound("Restaurants.Empty ", "Restaurants is  Empty !");
            }
            var list = new List<RestaurantResponce>();
            RestaurantResponce item = null;
            foreach (var restaurant in listrestaurant)
            {
                item = new RestaurantResponce(restaurant.Id,restaurant.Name,restaurant.Description,restaurant.BaseCatgoryId);

                list.Add(item);
            }


            return list;

        }
    }
}
