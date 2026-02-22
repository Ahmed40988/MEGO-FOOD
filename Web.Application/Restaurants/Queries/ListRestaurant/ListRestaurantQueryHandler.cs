using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.Common.Pagination;
using Web.Application.Restaurants.Contracts;

namespace Web.Application.Restaurants.Queries.ListRestaurant
{
    public class ListRestaurantQueryHandler(IRestaurantRepository restaurantCategoryRepository) : IRequestHandler<ListRestaurantQuerys, ErrorOr<PaginatedList<RestaurantResponce>>>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantCategoryRepository;

        public async Task<ErrorOr<PaginatedList<RestaurantResponce>>> Handle(ListRestaurantQuerys command, CancellationToken cancellationToken)
        {
            var listrestaurant = await _restaurantRepository.ListRestaurants(command.Filters);

            if(listrestaurant == null)
            {
                return Error.NotFound("Restaurants.Empty ", "Restaurants is  Empty !");
            }
            return listrestaurant;

        }
    }
}
