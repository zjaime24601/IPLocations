using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FreeIpClient;
using IPLocations.Api.Storage.Mongo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IPLocations.IntegrationTests;

[Collection(IPLocationsApiCollection.Name)]
public class GetIPLocationTests : IAsyncLifetime
{
    private readonly IPLocationsFixture _fixture;
    private readonly HttpClient _client;
    private readonly IMongoCollection<LocationDocument> _storageCollection;
    private readonly FreeIpStubClient _freeApiStub;

    public GetIPLocationTests(IPLocationsFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.CreateClient();
        _storageCollection = _fixture.GetLocationsStorage();
        _freeApiStub = _fixture.Services.GetRequiredService<IFreeIpClient>() as FreeIpStubClient;
    }

    public async Task InitializeAsync()
    {
        await _fixture.ClearLocationsStorage();
        _fixture.ClearCache();
        _freeApiStub.Reset();
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
    public async Task WhenIPLookupFailed_ThenReturnsUnavailable()
    {
        const string testIpAddress = "127.0.0.1";
        _freeApiStub.RegisterFailureIp(testIpAddress);
        var response = await _client.GetAsync($"/locations/{testIpAddress}");
        response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);

        var problemResponse = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemResponse.Should()
            .NotBeNull()
            .And.HaveDetail("IP lookup unavailable")
            .And.HaveValidTraceId();

        var storedLocations = await _storageCollection.Find(
            Builders<LocationDocument>.Filter.Eq(l => l.IpAddress, testIpAddress))
        .ToListAsync();
        storedLocations.Should().HaveCount(0);
    }

    [Fact]
    public async Task GivenPreviouslySuccessfulLookup_WhenExternalLookupFails_ThenReturnsPersistedLocation()
    {
        const string testIpAddress = "127.0.0.1";
        var response = await _client.GetAsync($"/locations/{testIpAddress}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _freeApiStub.RegisterFailureIp(testIpAddress);
        var response2 = await _client.GetAsync($"/locations/{testIpAddress}");
        response2.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GivenPreviousTransientFailure_WhenGettingIPLocation_ThenReturnsLocation()
    {
        const string testIpAddress = "127.0.0.1";
        _freeApiStub.RegisterFailureIp(testIpAddress);
        var response = await _client.GetAsync($"/locations/{testIpAddress}");
        response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);

        _freeApiStub.RegisterSuccessIp(testIpAddress);
        var response2 = await _client.GetAsync($"/locations/{testIpAddress}");
        response2.StatusCode.Should().Be(HttpStatusCode.OK);
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
