namespace IPLocations.Api.Locations.Domain;

public interface ILocationsService
{
    public Task<Location> GetLocationByIpAsync(string ipAddress);
}
