using IPLocations.Api.Locations;
using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api;

public static class ApiMappingExtensions
{
    public static LocationResponse ToApiResponse(this Location location)
        => new()
        {
            IpAddress = location.IpAddress,
            CountryCode = location.CountryCode,
            CountryName = location.CountryName,
            RegionName = location.RegionName,
            CityName = location.CityName,
            ZipCode = location.ZipCode,
            Latitude = location.Latitude,
            Longitude = location.Longitude
        };
}
