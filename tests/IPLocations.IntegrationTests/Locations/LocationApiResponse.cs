namespace IPLocations.IntegrationTests;

public class LocationApiResponse
{
    public string IpAddress { get; init; }

    public string CountryCode { get; init; }

    public string CountryName { get; init; }

    public string RegionName { get; init; }

    public string CityName { get; init; }

    public string ZipCode { get; init; }

    public double? Latitude { get; init; }

    public double? Longitude { get; init; }
}
