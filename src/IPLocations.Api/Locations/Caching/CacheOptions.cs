using System.ComponentModel.DataAnnotations;

namespace IPLocations.Api.Locations.Caching;

public class CacheOptions
{
    public bool Enabled { get; init; } = true;

    [Required]
    public long? DefaultExpirationSeconds { get; init; }

    public List<CachePrefixOptions> PrefixOptions { get; init; } = new();

    public class CachePrefixOptions
    {
        public string Prefix { get; set; }

        public long? ExpirationsSeconds { get; set; }
    }
}
