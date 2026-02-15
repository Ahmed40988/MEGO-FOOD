using Web.Application.MenuCategories.Commands.CreateMenuCategory;
using Web.Application.MenuCategories.Commands.DeleteMenuCategory;
using Web.Application.MenuCategories.Commands.UpdateMenuCategory;
using Web.Application.MenuCategories.MenuCategoryDTO;
using Web.Application.MenuCategories.Queries.GetMenuCategory;
using Web.Application.MenuCategories.Queries.listMenuCategory;

namespace Web.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoriesController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;


        /// <summary>
        /// Create New MenuCategory
        /// </summary>
        /// <response code="200">Created is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="409">MenuCategory  already exists with the same name</response>
        [HttpPost("Create_MenuCategory")]
        public async Task<IActionResult> CreateMenuCategory([FromBody] MenuCategoryRequest request)
        {
            var command = new CreateMenuCategoryCommand(
                request.Name,
                request.Description,
                request.Restaurantid);

            var result = await _mediator.Send(command);

            return result.Match(
         MenuCategory => Ok(MenuCategory),
         errors => ToProblem(errors));
        }


        [HttpGet("Get_All_MenuCategories")]
        public async Task<IActionResult> GetAllMenuCategories()
        {
            var Query = new listMenuCategoriesQuery();
            var result = await _mediator.Send(Query);
            return result.Match(
           list => Ok(list),
           errors => ToProblem(errors)
       );
        }

        /// <summary>
        /// Retrieves a specific MenuCategory by its ID, ensuring it belongs to the specified Restaurant.
        /// </summary>
        /// <remarks>
        /// Both <b>Id</b> (in the route) and <b>RestaurantId</b> (in the query) are required.
        /// This design enforces security by verifying ownership: a MenuCategory can only be accessed if it belongs to the given Restaurant.
        /// <br/><br/>
        /// Without this check, anyone with a MenuCategory ID could access any category, regardless of restaurant association.
        /// <br/><br/>
        /// <b>Example:</b> <c>GET /api/MenuCategories/Get_MenuCategoryBy_ID/2?RestaurantId=55</c>
        /// </remarks>
        /// <param name="Id">The MenuCategory unique identifier (from route).</param>
        /// <param name="RestaurantId">The Restaurant unique identifier (from query).</param>
        /// <returns>The MenuCategory details if found and associated with the Restaurant; otherwise, an error response.</returns>
        [HttpGet("Get_MenuCategoryBy_ID/{Id}")]
        public async Task<IActionResult> GetMenuCategoryByID([FromRoute] Guid Id, [FromQuery]Guid RestaurantId)
        {
            var Query = new GetMenuCategoryQuery(RestaurantId, Id);
            var result = await _mediator.Send(Query);
            return result.Match(
           item => Ok(item),
           errors => ToProblem(errors)
       );
        }


        /// <summary>
        /// Delete MenuCategory
        /// </summary>
        /// <response code="200">Deleted is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="404"> User Restaurant Owner not found. </response>
        /// <response code="404"> MenuCategory not found. </response>
        [HttpDelete("Delete_MenuCategory/{Id}")]
        public async Task<IActionResult> DeleteMenuCategory([FromRoute] Guid Id)
        {
            var AdminId = User.GetUserId();
            var command = new DeleteMenuCategoryCommand(AdminId, Id);
            var result = await _mediator.Send(command);
            return result.Match(
             _ => Ok(),
             errors => ToProblem(errors)
         );
        }


        /// <summary>
        /// Updated MenuCategory
        /// </summary>
        /// <response code="200">Updated is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="409">MenuCategory  already exists with the same name</response>

        [HttpPut("Update_MenuCategory/{Id}")]
        public async Task<IActionResult> UpdateMenuCategory([FromRoute] Guid Id, [FromBody] MenuCategoryRequest request)
        {
            var adminId = User.GetUserId();
            var command = new UpdateMenuCategoryCommand(Id, request.Name, request.Description, request.Restaurantid, adminId);
            var result = await _mediator.Send(command);
            return result.Match(
       Id => Ok(Id),
       errors => ToProblem(errors)
            );
        }
    }
}
