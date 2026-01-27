using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Application.Restaurants.Queries.GetRestaurant;
using Web.Domain.Restaurants;

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
