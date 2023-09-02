using IPLocations.Api.Locations.External;
using IPLocations.Api.Locations.Storage;

namespace IPLocations.Api.Locations.Domain;

public class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly IExternalLocationsProvider _externalLocationsProvider;
    private readonly ILogger<LocationsService> _logger;

    public LocationsService(
        ILocationsRepository locationsRepository,
        IExternalLocationsProvider externalLocationsProvider,
        ILogger<LocationsService> logger)
    {
        _locationsRepository = locationsRepository;
        _externalLocationsProvider = externalLocationsProvider;
        _logger = logger;
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
        {
            _logger.LogInformation("Falling back to value from persisted store.");
            return Result<Location>.Succeeded(persistedIpAddress);
        }

        return locationResult;
    }
}
