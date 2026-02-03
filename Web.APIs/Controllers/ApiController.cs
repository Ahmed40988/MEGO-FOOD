

namespace Web.APIs.Controllers
{
    //[ApiController]
    //public abstract class ApiController : ControllerBase
    //{
    //    protected IActionResult ToProblem(List<Error> errors)
    //    {
    //        if (errors.Count == 0)
    //        {
    //            return base.Problem();
    //        }

    //        if (errors.All(error => error.Type == ErrorType.Validation))
    //        {
    //            return ToValidationProblem(errors);
    //        }

    //        return ToProblem(errors[0]);
    //    }

    //    protected IActionResult ToProblem(Error error)
    //    {
    //        var statusCode = error.Type switch
    //        {
    //            ErrorType.Conflict => StatusCodes.Status409Conflict,
    //            ErrorType.Validation => StatusCodes.Status400BadRequest,
    //            ErrorType.NotFound => StatusCodes.Status404NotFound,
    //            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
    //            ErrorType.Failure => StatusCodes.Status404NotFound,
    //            _ => StatusCodes.Status500InternalServerError,
    //        };

    //        return base.Problem(
    //            statusCode: statusCode,
    //            title: error.Code,
    //            detail: error.Description);
    //    }

    //    protected IActionResult ToValidationProblem(List<Error> errors)
    //    {
    //        var modelStateDictionary = new ModelStateDictionary();

    //        foreach (var error in errors)
    //        {
    //            modelStateDictionary.AddModelError(
    //                error.Code,
    //                error.Description);
    //        }

    //        return base.ValidationProblem(modelStateDictionary);
    //    }
    //}


    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ToProblem(List<Error> errors)
        {
            if (errors.Count == 0)
                return ValidationProblem();

            return ToCustomValidationProblem(errors);
        }

        private IActionResult ToCustomValidationProblem(List<Error> errors)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelState.AddModelError(
                    error.Code,
                    error.Description
                );
            }

            var firstError = errors[0];

            var (statusCode, title, type) = MapError(firstError.Type);

            return ValidationProblem(
                modelStateDictionary: modelState,
                statusCode: statusCode,
                title: title,
                type: type
            );
        }

        private static (int statusCode, string title, string type) MapError(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => (
                    StatusCodes.Status400BadRequest,
                    "One or more validation errors occurred.",
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1"
                ),

                ErrorType.Forbidden => (
                    StatusCodes.Status403Forbidden,
                    "Access denied.",
                    "https://tools.ietf.org/html/rfc9110#section-15.5.4"
                ),

                ErrorType.NotFound => (
                    StatusCodes.Status404NotFound,
                    "Resource not found.",
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5"
                ),

                ErrorType.Conflict => (
                    StatusCodes.Status409Conflict,
                    "Conflict occurred.",
                    "https://tools.ietf.org/html/rfc9110#section-15.5.10"
                ),
                ErrorType.Failure => (
                StatusCodes.Status404NotFound,
                "Enter Correct Values!",
                 "https://tools.ietf.org/html/rfc9110#section-15.5.5"
                 ),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred.",
                    "https://tools.ietf.org/html/rfc9110#section-15.6.1"
                )
            };
        }
    }

}
