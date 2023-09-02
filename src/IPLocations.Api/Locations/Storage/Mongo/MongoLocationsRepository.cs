using IPLocations.Api.Locations.Domain;
using IPLocations.Api.Locations.Storage;
using MongoDB.Driver;

namespace IPLocations.Api.Storage.Mongo;

public class MongoLocationsRepository : ILocationsRepository
{
    private readonly IMongoCollection<LocationDocument> _collection;

    public MongoLocationsRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<LocationDocument>("ipLocations");
    }

    public async Task<Location> GetLocationFromIp(string ipAddress)
    {
        var storedLocation = await _collection
            .Find(Builders<LocationDocument>.Filter.Eq(l => l.IpAddress, ipAddress))
            .SortByDescending(l => l.Id)
            .FirstOrDefaultAsync();

        return new()
        {
            IpAddress = storedLocation.IpAddress,
            CountryCode = storedLocation.CountryCode,
            CountryName = storedLocation.CountryName,
            RegionName = storedLocation.RegionName,
            CityName = storedLocation.CityName,
            ZipCode = storedLocation.ZipCode,
            Latitude = storedLocation.Latitude,
            Longitude = storedLocation.Longitude
        };
    }

    public async Task StoreIpLocation(Location location)
    {
        var document = new LocationDocument()
        {
            IpAddress = location.IpAddress,
            CountryCode = location.CountryCode,
            CountryName = location.CountryName,
            RegionName = location.RegionName,
            CityName = location.CityName,
            ZipCode = location.ZipCode,
            Latitude = location.Latitude,
            Longitude = location.Longitude
        };

        await _collection.InsertOneAsync(document);
    }
}
