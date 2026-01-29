using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Admins.Queries.DeleteUser;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController(ISender sender) : ApiController
    {
        private readonly ISender _Mediator = sender;

        [HttpPost("DeleteUserByEmail")]
        public async Task<IActionResult> DeleteUserbyEmail(string email)
        {
            var adminid=User.GetUserId();
            var result=await _Mediator.Send(new DeleteUserbyEmailQuery(adminid, email));
            return result.Match(
                 _ => Ok(),
                 errors => ToProblem(errors)
             );

        }
    }
}
