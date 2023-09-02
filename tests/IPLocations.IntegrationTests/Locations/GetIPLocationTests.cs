using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace IPLocations.IntegrationTests;

[Collection(IPLocationsApiCollection.Name)]
public class GetIPLocationTests
{
    private readonly HttpClient _client;

    public GetIPLocationTests(IPLocationsFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task GivenValidIPAddress_WhenGettingIPLocation_ThenReturnsLocation()
    {
        var response = await _client.GetAsync("/locations/127.0.0.1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var locationResponse = await response.Content.ReadFromJsonAsync<LocationApiResponse>();
        locationResponse.Should().BeEquivalentTo(new LocationApiResponse()
        {
            IpAddress = "127.0.0.1",
            CountryCode = "GB",
            CountryName = "United Kingom",
            RegionName = "England",
            CityName = "Nottingham",
            ZipCode = "NG7",
            Latitude = 3.456,
            Longitude = 6.543
        });
    }

    [Fact]
    public async Task GivenInvalidIPAddress_WhenGettingIPLocation_ThenReturnsBadRequest()
    {
        var response = await _client.GetAsync("/locations/Gibberish");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemResponse = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemResponse.Should()
            .NotBeNull()
            .And.HaveDetail("IP address is invalid")
            .And.HaveValidTraceId();
    }
}
