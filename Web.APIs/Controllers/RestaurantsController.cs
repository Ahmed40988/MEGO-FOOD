
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Restaurants.Commands.CreateRestaurants;
using Web.Application.RestaurantCategories.Contracts;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantRequest request)
        {
            var command = new CreateRestaurantsCommand(
                request.name,
                request.description,
                request.userId);

           var  result = await _mediator.Send(command);

            return result.Match(
         category => Ok(category),
         errors => ToProblem(errors));
        }

    }
}
