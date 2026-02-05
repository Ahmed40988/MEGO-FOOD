using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Restaurants.Queries.GetRestaurant
{
    public class GetRestaurantQueryValidator:AbstractValidator<GetRestaurantQuery>
    {

        public GetRestaurantQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Restaurant Id is required.");
        }
    }
}
