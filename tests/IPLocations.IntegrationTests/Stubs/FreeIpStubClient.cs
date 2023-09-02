using FreeIpClient;

namespace IPLocations.IntegrationTests;

public class FreeIpStubClient : IFreeIpClient
{
    private readonly Dictionary<string, Func<IpLocation>> _ipHandlers = new();

    public Task<IpLocation> LookupIpLocation(string ipAddress)
    {
        var location = _ipHandlers.TryGetValue(ipAddress, out var lookupExecutor)
            ? lookupExecutor()
            : BuildSuccessfulResponse(ipAddress);
        return Task.FromResult(location);
    }

    public void Reset()
    {
        _ipHandlers.Clear();
    }

    public void RegisterSuccessIp(string ipAddress)
    {
        _ipHandlers[ipAddress] = () => BuildSuccessfulResponse(ipAddress);
    }

    public void RegisterFailureIp(string ipAddress)
    {
        _ipHandlers[ipAddress] = () => throw new FreeIpException("Failed to lookup IP address from FreeIP");
    }

    private static IpLocation BuildSuccessfulResponse(string ipAddress)
        => new()
        {
            IpVersion = 4,
            IpAddress = ipAddress,
            Latitude = 3.456,
            Longitude = -6.543,
            CountryName = "United Kingdom of Great Britain and Northern Ireland",
            CountryCode = "GB",
            TimeZone = "+01:00",
            ZipCode = "NG1",
            CityName = "Nottingham",
            RegionName = "England",
            Continent = "Europe",
            ContinentCode = "EU"
        };
}
