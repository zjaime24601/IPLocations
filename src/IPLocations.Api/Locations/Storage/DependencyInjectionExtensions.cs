using IPLocations.Api.Locations.Storage.Mongo;
using IPLocations.Api.Storage.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IPLocations.Api.Locations.Storage;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddLocationsStorage(this IServiceCollection services)
    {
        services.AddOptions<MongoOptions>()
            .BindConfiguration("Mongo")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(options.ConnectionString);
        });
        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return sp.GetRequiredService<MongoClient>().GetDatabase(options.DatabaseName);
        });
        services.AddSingleton<ILocationsRepository, MongoLocationsRepository>();
        return services;
    }
}
