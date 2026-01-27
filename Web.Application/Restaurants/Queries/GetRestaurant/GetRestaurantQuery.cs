using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Restaurants;

namespace Web.Application.Restaurants.Queries.GetRestaurant
{
    public record GetRestaurantQuery(Guid CategoryId):IRequest<ErrorOr<Restaurant>>;
}
