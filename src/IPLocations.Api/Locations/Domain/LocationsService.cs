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

    public async Task<Result<Location>> GetLocationByIpAsync(string ipAddress)
    {
        var locationResult = await _externalLocationsProvider.GetIpLocation(ipAddress);
        if (locationResult.Success)
        {
            await _locationsRepository.StoreIpLocation(locationResult.Value);
            return locationResult;
        }

        var persistedIpAddress = await _locationsRepository.GetLocationFromIp(ipAddress);
        if (persistedIpAddress != null)
            return Result<Location>.Succeeded(persistedIpAddress);

        return locationResult;
    }
}
