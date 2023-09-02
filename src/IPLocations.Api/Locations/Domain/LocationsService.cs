using IPLocations.Api.Locations.Domain;
using IPLocations.Api.Locations.Storage;

namespace IPLocations.Api.Locations.Domain;

public class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _locationsRepository;

    public LocationsService(
        ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }

    public async Task<Location> GetLocationByIpAsync(string ipAddress)
    {
        // TODO Add error handling to fallback to persisted value
        var location = await GetLocationFromExternalProvider(ipAddress);
        await _locationsRepository.StoreIpLocation(location);
        return location;
    }

    private Task<Location> GetLocationFromExternalProvider(string ipAddress) => Task.FromResult(new Location
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
