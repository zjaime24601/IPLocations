using IPLocations.Api.Locations;

namespace IPLocations.Api;

public interface ILocationsCache
{
    Task<LocationResponse> TryGetOrAddAsync(string key, Func<Task<LocationResponse>> create);
}
