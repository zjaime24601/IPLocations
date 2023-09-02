using IPLocations.Api.Locations.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IPLocations.Api.Locations;

[ApiController]
[Route("locations")]
public class LocationsController : ControllerBase
{
    [HttpGet("{ipAddress}")]
    public async Task<IActionResult> GetLocation(
        [FromRoute] string ipAddress,
        [FromServices] ILocationsService locationsService)
    {
        if (!ValidationHelpers.IsValidIPAddress(ipAddress))
        {
            return Problem("IP address is invalid", statusCode: StatusCodes.Status400BadRequest);
        }

        var location = await locationsService.GetLocationByIpAsync(ipAddress);
        return Ok(location.ToApiResponse());
    }
}
