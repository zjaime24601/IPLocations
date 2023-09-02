
using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api.Locations;

public class StaticLocationsService : ILocationsService
{
    public Task<Location> GetLocationByIpAsync(string ipAddress) => Task.FromResult(new Location
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
