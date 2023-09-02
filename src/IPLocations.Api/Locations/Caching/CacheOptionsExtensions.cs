namespace IPLocations.Api.Locations.Caching;

public static class CacheOptionsExtensions
{
    public static TimeSpan GetExpiryForKey(this CacheOptions options, string key)
    {
        var prefixOption = options.PrefixOptions
            .Where(o => o.ExpirationsSeconds > 0
                && key.StartsWith(o.Prefix))
            .OrderByDescending(o => o.Prefix.Length)
            .FirstOrDefault();
        var expirySeconds = prefixOption?.ExpirationsSeconds ?? options.DefaultExpirationSeconds;
        return TimeSpan.FromSeconds(expirySeconds.Value);
    }
}
