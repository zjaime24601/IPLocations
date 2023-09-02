using System.ComponentModel.DataAnnotations;

namespace IPLocations.Api;

public class CacheOptions
{
    private static readonly int s_expirationDefaultSeconds = (int)TimeSpan.FromDays(1).TotalSeconds;

    public bool Enabled { get; init; } = true;

    public int DefaultExpirationSeconds { get; init; } = s_expirationDefaultSeconds;

    public List<CachePrefixOptions> PrefixOptions { get; init; } = new();

    public class CachePrefixOptions
    {
        [Required]
        public string Prefix { get; set; }

        [Required]
        public long ExpirationsSeconds { get; set; }
    }
}
