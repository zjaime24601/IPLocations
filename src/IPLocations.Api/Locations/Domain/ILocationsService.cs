namespace IPLocations.Api.Locations.Domain;

public interface ILocationsService
{
    Task<Location> GetLocationByIpAsync(string ipAddress);
}
