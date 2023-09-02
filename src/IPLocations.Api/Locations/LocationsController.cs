using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace IPLocations.Api;

[ApiController]
[Route("locations")]
public class LocationsController : ControllerBase
{
    [HttpGet("{ipAddress}")]
    public async Task<IActionResult> GetLocation([FromRoute] string ipAddress)
    {
        if (!ValidationHelpers.IsValidIPAddress(ipAddress))
        {
            return Problem("IP address is invalid", statusCode: StatusCodes.Status400BadRequest);
        }

        await Task.CompletedTask;

        return Ok(new LocationResponse
        {
            IpAddress = "127.0.0.1",
            CountryCode = "GB",
            CountryName = "United Kingom",
            RegionName = "England",
            CityName = "Nottingham",
            ZipCode = "NG7",
            Latitude = 3.456,
            Longitude = 6.543
        });
    }
}
