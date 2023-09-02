using IPLocations.Api.Locations.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IPLocations.Api.Locations;

[ApiController]
[Route("locations")]
public class LocationsController : ControllerBase
{
    private const string CachePrefix = "locations/v1/";

    [HttpGet("{ipAddress}")]
    public async Task<IActionResult> GetLocation(
        [FromRoute] string ipAddress,
        [FromServices] ILocationsCache locationsCache,
        [FromServices] ILocationsService locationsService)
    {
        if (!ValidationHelpers.IsValidIPAddress(ipAddress))
        {
            return Problem("IP address is invalid", statusCode: StatusCodes.Status400BadRequest);
        }

        var locationResponse = await locationsCache.TryGetOrAddAsync(CachePrefix + ipAddress, async ()
            => (await locationsService.GetLocationByIpAsync(ipAddress)).ToApiResponse());
        return Ok(locationResponse);
    }
}
