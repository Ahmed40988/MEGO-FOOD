using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Restaurants;

namespace Web.Application.Restaurants.Queries.ListRestaurant
{
    public record ListRestaurantQuerys:IRequest<ErrorOr<List<Restaurant>>>;
}
