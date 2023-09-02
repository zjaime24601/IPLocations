using IPLocations.Api.Locations.Domain;

namespace IPLocations.Api.Locations;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddLocations(this IServiceCollection services)
    {
        return services.AddScoped<ILocationsService, StaticLocationsService>();
    }
}
