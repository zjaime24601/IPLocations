using System.ComponentModel.DataAnnotations;

namespace IPLocations.Api.Locations.Storage.Mongo;

public class MongoOptions
{
    [Required]
    public string ConnectionString { get; set; }

    [Required]
    public string DatabaseName { get; set; }
}
