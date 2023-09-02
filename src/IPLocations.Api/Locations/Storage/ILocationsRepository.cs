using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api.Locations.Storage;

public interface ILocationsRepository
{
    Task<Location> GetLocationFromIp(string ipAddress);

    Task StoreIpLocation(Location location);
}
