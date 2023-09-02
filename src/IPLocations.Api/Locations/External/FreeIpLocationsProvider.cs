using FreeIpClient;
using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api.Locations.External;

public class FreeIpLocationsProvider : IExternalLocationsProvider
{
    private readonly IFreeIpClient _freeIpClient;

    public FreeIpLocationsProvider(IFreeIpClient freeIpClient)
    {
        _freeIpClient = freeIpClient;
    }

    public async Task<Location> GetIpLocation(string ipAddress)
    {
        var ipLocation = await _freeIpClient.LookupIpLocation(ipAddress);
        return new()
        {
            IpAddress = ipAddress,
            CountryCode = ipLocation.CountryCode,
            CountryName = ipLocation.CountryName,
            RegionName = ipLocation.RegionName,
            CityName = ipLocation.CityName,
            ZipCode = ipLocation.ZipCode,
            Latitude = ipLocation.Latitude,
            Longitude = ipLocation.Longitude
        };
    }
}
