using FluentAssertions;
using IPLocations.Api.Locations;
using IPLocations.Api.Locations.Domain;

namespace IPLocations.UnitTests;

public class ApiMappingExtensionsTests
{
    [Fact]
    public void GivenLocation_WhenMappingToApiResponse_ThenReturnsValidApiResponse()
    {
        var random = new Random();
        var location = new Location
        {
            IpAddress = Guid.NewGuid().ToString(),
            CountryCode = Guid.NewGuid().ToString(),
            CountryName = Guid.NewGuid().ToString(),
            RegionName = Guid.NewGuid().ToString(),
            CityName = Guid.NewGuid().ToString(),
            ZipCode = Guid.NewGuid().ToString(),
            Latitude = random.NextLatitude(),
            Longitude = random.NextLongitude()
        };
        var locationApiResponse = location.ToApiResponse();
        locationApiResponse.Should().BeEquivalentTo(location);
    }
}
