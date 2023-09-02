using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using IPLocations.Api.Storage.Mongo;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace IPLocations.IntegrationTests;

[Collection(IPLocationsApiCollection.Name)]
public class GetIPLocationTests : IAsyncLifetime
{
    private readonly IPLocationsFixture _fixture;
    private readonly HttpClient _client;
    private IMongoCollection<LocationDocument> _storageCollection;

    public GetIPLocationTests(IPLocationsFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.CreateClient();
        _storageCollection = _fixture.GetLocationsStorage();
    }

    public async Task InitializeAsync()
    {
        await _fixture.ClearLocationsStorage();
        _fixture.ClearCache();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GivenValidIPAddress_WhenGettingIPLocation_ThenReturnsLocation()
    {
        const string testIpAddress = "127.0.0.1";
        var response = await _client.GetAsync($"/locations/{testIpAddress}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var locationResponse = await response.Content.ReadFromJsonAsync<LocationApiResponse>();
        locationResponse.Should().BeEquivalentTo(new LocationApiResponse()
        {
            IpAddress = testIpAddress,
            CountryCode = "GB",
            CountryName = "United Kingdom of Great Britain and Northern Ireland",
            RegionName = "England",
            CityName = "Nottingham",
            ZipCode = "NG1",
            Latitude = 3.456,
            Longitude = -6.543
        });

        var storedLocations = await _storageCollection.Find(
            Builders<LocationDocument>.Filter.Eq(l => l.IpAddress, testIpAddress))
        .ToListAsync();
        storedLocations.Should().HaveCount(1);
        storedLocations.First().Should().BeEquivalentTo(locationResponse);
    }

    [Fact]
    public async Task GivenPreviouslyRequestedIPAddress_WhenGettingIPLocation_ThenOnly1IPAddressIsPersisted()
    {
        const string testIpAddress = "127.0.0.1";
        var response = await _client.GetAsync($"/locations/{testIpAddress}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var response2 = await _client.GetAsync($"/locations/{testIpAddress}");
        response2.StatusCode.Should().Be(HttpStatusCode.OK);

        var storedLocations = await _storageCollection.Find(
            Builders<LocationDocument>.Filter.Eq(l => l.IpAddress, testIpAddress))
        .ToListAsync();
        storedLocations.Should().HaveCount(1);
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
