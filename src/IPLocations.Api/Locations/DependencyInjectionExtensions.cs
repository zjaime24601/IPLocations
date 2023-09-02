using IPLocations.Api.Locations.Domain;
using IPLocations.Api.Locations.Storage;

namespace IPLocations.Api.Locations;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddLocations(this IServiceCollection services)
    {
        return services
            .AddLocationsStorage()
            .AddLocationsCache()
            .AddScoped<ILocationsService, LocationsService>();
    }
}
