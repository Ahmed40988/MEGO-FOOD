using System.Text.RegularExpressions;
using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.BaseCategories.Commands.CreateBaseCategory;
using Web.Application.BaseCategories.Commands.DeleteBaseCategory;
using Web.Application.BaseCategories.Commands.UpdateBaseCategory;
using Web.Application.BaseCategories.Queries.GetBaseCategories;
using Web.Application.BaseCategories.Queries.GetBaseCategoryById;
using Web.Application.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BaseCategoriesController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        /// <summary>
        /// Create New Base Category
        /// </summary>
        /// <response code="200">Created is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="409">Base Category  already exists with the same name</response>
        [HttpPost("Create_BaseCategory")]
        public async Task<IActionResult> CreateBaseCategory(BaseCategoryRequest request)
        {
            var userid = User.GetUserId();
            var command = new CreateBaseCategoryCommand(request.Name, request.Description, userid);
            var result = await _mediator.Send(command);
            return result.Match(
             Id => Ok(Id),
             errors => ToProblem(errors)
         );
        }
        [HttpGet("Get_All_BaseCategories")]
        public async Task<IActionResult> GetAllBaseCategories([FromQuery]RequestFilters filters)
        {
            var Query = new GetBaseCategoriesQuery(filters);
            var result = await _mediator.Send(Query);
            return result.Match(
           list => Ok(list),
           errors => ToProblem(errors));
        }


        [HttpGet("Get_BaseCategoryBy_ID/{Id}")]
        public async Task<IActionResult> GetBaseCategoryByID([FromRoute] Guid Id)
        {
            var Query = new GetBaseCategoryByIdQuery(Id);
            var result = await _mediator.Send(Query);
            return result.Match(
           item => Ok(item),
           errors => ToProblem(errors)
       );
        }




        /// <summary>
        /// Delete Base Category
        /// </summary>
        /// <response code="200">Deleted is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="404"> BaseCategory not found. </response>
        [HttpDelete("Delete_BaseCategory/{Id}")]
        public async Task<IActionResult> DeleteBaseCategory([FromRoute]Guid Id)
        {
            var AdminId = User.GetUserId();
            var command = new DeleteBaseCategoryCommand(Id, AdminId);
            var result = await _mediator.Send(command);
            return result.Match(
             _ => Ok(),
             errors => ToProblem(errors)
         );
        }


        /// <summary>
        /// Updated Base Category
        /// </summary>
        /// <response code="200">Updated is  successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401"> Unauthorized. JWT token is missing or invalid. </response>
        /// <response code="409">Base Category  already exists with the same name</response>

        [HttpPut("Update_BaseCategory/{Id}")]
        public async Task<IActionResult> UpdateBaseCategory([FromRoute]Guid Id,[FromBody]BaseCategoryRequest request)
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
