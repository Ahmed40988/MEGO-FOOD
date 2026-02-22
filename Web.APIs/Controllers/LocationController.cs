using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Adresses.AddressDTO;
using Web.Application.Adresses.Commands.SetUserLocation;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ApiController
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>Set user location by latitude and longitude.</summary>
        /// <remarks> Set user location by latitude and longitude The system will attempt to retrieve the address using reverse geocoding,
        /// but if it fails, the address will be set to null. 
        /// This endpoint allows users to update their location information
        /// , which can be used for various features such as personalized content or location-based services. </remarks>
        /// <response code="200">Set Location is successfully and return Ex:"addressId": 1, "address": "ميدان التحرير, قصر الدوباره, باب اللوق, القاهرة, 11519, مصر"</response>
        [Authorize]
        [HttpPost("set_Location")]
        public async Task<IActionResult> SetLocation(SetLocationRequest request)
        {
            var userId = User.GetUserId();

            var result = await _mediator.Send(
                new SetUserLocationCommand(
                    userId,
                    request.Lat,
                    request.Lng
                ));

            return result.Match(
        location => Ok(location),
        errors => ToProblem(errors));
        }
    }
}


