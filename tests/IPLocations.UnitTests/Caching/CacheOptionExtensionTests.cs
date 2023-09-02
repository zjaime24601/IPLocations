using FluentAssertions;
using IPLocations.Api.Locations.Caching;

namespace IPLocations.UnitTests.Caching;

public class CacheOptionExtensionTests
{
    public class GetExpiryForKey
    {
        [Theory]
        [InlineData("brand-new-key", 600)]
        [InlineData("locations/v1/192.168.0.1", 100)]
        [InlineData("locations/v1/127.0.0.1", 200)]
        [InlineData("locations/v2/127.0.0.1", 300)]
        public void GivenCacheOptions_WhenResolvingExpiry_ReturnsCorrectTime(string cacheKey, long expectedExpirySeconds)
        {
            var cacheOptions = new CacheOptionsBuilder()
                .WithDefaultExpiration(600)
                .WithPrefixOptions("locations/v1/127", 200)
                .WithPrefixOptions("locations/v1", 100)
                .WithPrefixOptions("locations/v2", 300)
                .Build();

            cacheOptions.GetExpiryForKey(cacheKey)
                .Should().Be(TimeSpan.FromSeconds(expectedExpirySeconds));
        }
    }
}
