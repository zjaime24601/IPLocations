using FreeIpClient;
using IPLocations.Api.Locations.Domain;
using IPLocations.Api.Locations.External;
using IPLocations.Api.Locations.Storage;

namespace IPLocations.Api.Locations.Domain;

public class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly IExternalLocationsProvider _externalLocationsProvider;

    public LocationsService(
        ILocationsRepository locationsRepository,
        IExternalLocationsProvider externalLocationsProvider)
    {
        _locationsRepository = locationsRepository;
        _externalLocationsProvider = externalLocationsProvider;
    }

    public async Task<Location> GetLocationByIpAsync(string ipAddress)
    {
        // TODO Add error handling to fallback to persisted value
        var location = await _externalLocationsProvider.GetIpLocation(ipAddress);
        await _locationsRepository.StoreIpLocation(location);
        return location;
    }
}
