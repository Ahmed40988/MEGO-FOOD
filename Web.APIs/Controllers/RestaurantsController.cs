using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.BaseCategories.Commands.DeleteBaseCategory;
using Web.Application.BaseCategories.Commands.UpdateBaseCategory;
using Web.Application.BaseCategories.Queries.GetBaseCategories;
using Web.Application.BaseCategories.Queries.GetBaseCategoryById;
using Web.Application.Restaurants.Commands.CreateRestaurants;
using Web.Application.Restaurants.Commands.DeleteRestaurants;
using Web.Application.Restaurants.Contracts;
using Web.Application.Restaurants.Queries.GetRestaurant;
using Web.Application.Restaurants.Queries.ListRestaurant;

namespace Web.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;


        /// <summary>
        /// Create New Restaurant
        /// </summary>
        /// <response code="200">Created is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="409">Restaurant  already exists with the same name</response>
        [HttpPost("Create_Restaurant")]
        public async Task<IActionResult> CreateRestaurant([FromBody]RestaurantRequest request)
        {
            var UserId=User.GetUserId();
            var command = new CreateRestaurantsCommand(
                request.Name,
                request.Description,
                UserId,
                request.BaseCatgoryId);

            var result = await _mediator.Send(command);

            return result.Match(
         Restaurant => Ok(Restaurant),
         errors => ToProblem(errors));
        }


        [HttpGet("Get_All_Restaurantes")]
        public async Task<IActionResult> GetAllRestaurantes()
        {
            var Query = new ListRestaurantQuerys();
            var result = await _mediator.Send(Query);
            return result.Match(
           list => Ok(list),
           errors => ToProblem(errors)
       );
        }


        [HttpGet("Get_RestaurantBy_ID/{Id}")]
        public async Task<IActionResult> GetRestaurantByID([FromRoute] Guid Id)
        {
            var Query = new GetRestaurantQuery(Id);
            var result = await _mediator.Send(Query);
            return result.Match(
           item => Ok(item),
           errors => ToProblem(errors)
       );
        }


        /// <summary>
        /// Delete Restaurant
        /// </summary>
        /// <response code="200">Deleted is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="404"> User Restaurant Owner not found. </response>
        /// <response code="404"> Restaurant not found. </response>
        [HttpDelete("Delete_Restaurant/{Id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] Guid Id)
        {
            var AdminId = User.GetUserId();
            var command = new DeleteRestaurantCommand(Id, AdminId);
            var result = await _mediator.Send(command);
            return result.Match(
             _ => Ok(),
             errors => ToProblem(errors)
         );
        }


        /// <summary>
        /// Updated Restaurant
        /// </summary>
        /// <response code="200">Updated is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="409">Restaurant  already exists with the same name</response>

        [HttpPut("Update_Restaurant/{Id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] Guid Id, [FromBody] RestaurantRequest request)
        {
            var adminId = User.GetUserId();
            var command = new UpdateBaseCategoryCommand(Id, request.Name, request.Description, adminId);
            var result = await _mediator.Send(command);
            return result.Match(
       Id => Ok(Id),
       errors => ToProblem(errors)
            );
        }



    }
}
