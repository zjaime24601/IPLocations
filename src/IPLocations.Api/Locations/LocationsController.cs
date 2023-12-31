﻿using IPLocations.Api.Locations.Caching;
using IPLocations.Api.Locations.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IPLocations.Api.Locations;

[ApiController]
[Route("locations")]
public class LocationsController : ControllerBase
{
    private const string CachePrefix = "locations/v1/";

    [HttpGet("{ipAddress}")]
    [ProducesResponseType(typeof(LocationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetLocation(
        [FromRoute] string ipAddress,
        [FromServices] ILocationsCache locationsCache,
        [FromServices] ILocationsService locationsService)
    {
        if (!ValidationHelpers.IsValidIPAddress(ipAddress))
        {
            return Problem("IP address is invalid", instance: ipAddress, statusCode: StatusCodes.Status400BadRequest);
        }

        var locationResponse = await locationsCache.TryGetOrAddAsync(CachePrefix + ipAddress, async () =>
        {
            var result = await locationsService.GetLocationByIpAsync(ipAddress);
            return result.Success
                ? result.Value.ToApiResponse()
                : null;
        });

        return locationResponse != null
            ? Ok(locationResponse)
            : Problem("IP lookup unavailable", statusCode: StatusCodes.Status503ServiceUnavailable);
    }
}
