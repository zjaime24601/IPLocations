using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api.Locations.External;

public interface IExternalLocationsProvider
{
    Task<Location> GetIpLocation(string ipAddress);
}
