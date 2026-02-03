using Web.Application.Admins.Queries.DeleteUser;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController(ISender sender) : ApiController
    {
        private readonly ISender _Mediator = sender;


        /// <summary> Delete User By Email . </summary>
        /// <remarks> For Admins only </remarks>
        /// <response code="200">USer is successful Deleted</response>
        /// <response code="404">userEmail is not found</response>

        [HttpDelete("DeleteUserByEmail")]
        public async Task<IActionResult> DeleteUserbyEmail(string email)
        {
            var adminid = User.GetUserId();
            var result = await _Mediator.Send(new DeleteUserbyEmailQuery(adminid, email));
            return result.Match(
                 _ => Ok(),
                 errors => ToProblem(errors)
             );

        }
    }
}
