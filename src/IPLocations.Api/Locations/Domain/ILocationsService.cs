namespace IPLocations.Api.Locations.Domain;

public interface ILocationsService
{
    Task<Result<Location>> GetLocationByIpAsync(string ipAddress);
}
