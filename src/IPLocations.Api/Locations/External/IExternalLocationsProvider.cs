using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api.Locations.External;

public interface IExternalLocationsProvider
{
    Task<Result<Location>> GetIpLocation(string ipAddress);
}
