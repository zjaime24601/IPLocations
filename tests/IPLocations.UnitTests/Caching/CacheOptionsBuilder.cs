using IPLocations.Api.Locations.Caching;

namespace IPLocations.UnitTests.Caching;

public class CacheOptionsBuilder
{
    private readonly List<CacheOptions.CachePrefixOptions> _prefixOptions = new();
    private long? _defaultExpirationSeconds;

    public CacheOptionsBuilder WithDefaultExpiration(long? expirationSeconds)
    {
        _defaultExpirationSeconds = expirationSeconds;
        return this;
    }

    public CacheOptionsBuilder WithPrefixOptions(string prefix, long? expirationSeconds)
    {
        _prefixOptions.Add(new()
        {
            Prefix = prefix,
            ExpirationsSeconds = expirationSeconds
        });
        return this;
    }

    public CacheOptions Build()
    {
        return new()
        {
            DefaultExpirationSeconds = _defaultExpirationSeconds ?? 60,
            PrefixOptions = _prefixOptions
        };
    }
}
