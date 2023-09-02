using MongoDB.Bson;

namespace IPLocations.Api.Storage.Mongo;

public class LocationDocument
{
    public ObjectId Id { get; init; } = ObjectId.Empty;

    public string IpAddress { get; set; }

    public string CountryCode { get; set; }

    public string CountryName { get; set; }

    public string RegionName { get; set; }

    public string CityName { get; set; }

    public string ZipCode { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
