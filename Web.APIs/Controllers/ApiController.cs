using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.APIs.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ToProblem(List<Error> errors)
        {
            if (errors.Count == 0)
            {
                return base.Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ToValidationProblem(errors);
            }

            return ToProblem(errors[0]);
        }

        protected IActionResult ToProblem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            return base.Problem(
                statusCode: statusCode,
                title: error.Code,
                detail: error.Description);
        }

        protected IActionResult ToValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }

            return base.ValidationProblem(modelStateDictionary);
        }
    }
}
