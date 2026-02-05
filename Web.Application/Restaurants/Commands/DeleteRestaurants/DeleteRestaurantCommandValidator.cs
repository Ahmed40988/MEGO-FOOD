using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Restaurants.Commands.DeleteRestaurants
{
    public class DeleteRestaurantCommandValidator:AbstractValidator<DeleteRestaurantCommand>
    {
        public DeleteRestaurantCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty.");
        }

    }
}
