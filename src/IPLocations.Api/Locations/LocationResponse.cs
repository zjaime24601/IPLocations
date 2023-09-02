namespace IPLocations.Api;

public class LocationResponse
{
    public required string IpAddress { get; init; }

    public required string CountryCode { get; init; }

    public required string CountryName { get; init; }

    public required string RegionName { get; init; }

    public required string CityName { get; init; }

    public required string ZipCode { get; init; }

    public required double Latitude { get; init; }

    public required double Longitude { get; init; }
}
