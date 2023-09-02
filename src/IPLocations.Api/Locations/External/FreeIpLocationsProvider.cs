using FreeIpClient;
using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api.Locations.External;

public class FreeIpLocationsProvider : IExternalLocationsProvider
{
    private readonly IFreeIpClient _freeIpClient;
    private readonly ILogger<FreeIpLocationsProvider> _logger;

    public FreeIpLocationsProvider(
        IFreeIpClient freeIpClient,
        ILogger<FreeIpLocationsProvider> logger)
    {
        _freeIpClient = freeIpClient;
        _logger = logger;
    }

    public async Task<Result<Location>> GetIpLocation(string ipAddress)
    {
        try
        {
            var ipLocation = await _freeIpClient.LookupIpLocation(ipAddress);
            return Result<Location>.Succeeded(new()
            {
                IpAddress = ipAddress,
                CountryCode = ipLocation.CountryCode,
                CountryName = ipLocation.CountryName,
                RegionName = ipLocation.RegionName,
                CityName = ipLocation.CityName,
                ZipCode = ipLocation.ZipCode,
                Latitude = ipLocation.Latitude,
                Longitude = ipLocation.Longitude
            });
        }
        catch (FreeIpException ex)
        {
            _logger.LogError(ex, "Failed to lookup location for '{IPAddress}' from FreeIP", ipAddress);
            return Result<Location>.Failed();
        }
    }
}
