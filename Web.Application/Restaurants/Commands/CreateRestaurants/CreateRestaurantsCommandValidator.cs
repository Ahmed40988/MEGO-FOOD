using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantsCommandValidator:AbstractValidator<CreateRestaurantsCommand>
    {
        public CreateRestaurantsCommandValidator()
        {
            RuleFor(x => x.name)
                .MinimumLength(3)
                .MaximumLength(15);

            RuleFor(x => x.description)
                .MinimumLength(3)
                .MaximumLength(50);
                  
        }

    }
}
