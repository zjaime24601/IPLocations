using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace IPLocations.Api.Locations.Caching;

public class InMemoryLocationsCache : ILocationsCache
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheOptions _cacheOptions;

    public InMemoryLocationsCache(
        IMemoryCache memoryCache,
        IOptions<CacheOptions> cacheOptions)
    {
        _memoryCache = memoryCache;
        _cacheOptions = cacheOptions.Value;
    }

    public async Task<LocationResponse> TryGetOrAddAsync(string key, Func<Task<LocationResponse>> create)
    {
        if (!_cacheOptions.Enabled)
            return await create();

        return await _memoryCache.GetOrCreateAsync(key, async ce =>
        {
            ce.AbsoluteExpirationRelativeToNow = _cacheOptions.GetExpiryForKey(key);
            return await create();
        });
    }
}
