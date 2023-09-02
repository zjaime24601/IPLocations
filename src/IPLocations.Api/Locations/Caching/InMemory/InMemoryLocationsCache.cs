using IPLocations.Api.Locations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace IPLocations.Api;

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
        return await _memoryCache.GetOrCreateAsync(key, async ce =>
        {
            ce.AbsoluteExpirationRelativeToNow = GetCacheExpiry(key);
            return await create();
        });
    }

    private TimeSpan GetCacheExpiry(string key)
    {
        var prefixOption = _cacheOptions.PrefixOptions.Where(o => key.StartsWith(o.Prefix))
        .OrderByDescending(o => o.Prefix)
        .FirstOrDefault();
        var expirySeconds = prefixOption?.ExpirationsSeconds ?? _cacheOptions.DefaultExpirationSeconds;
        return TimeSpan.FromSeconds(expirySeconds);
    }
}
