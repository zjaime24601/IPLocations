using FreeIpClient;

namespace IPLocations.Api.Locations.External;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddExternalLocationsProvider(this IServiceCollection services)
    {
        return services
            .AddFreeIpClient()
            .AddSingleton<IExternalLocationsProvider, FreeIpLocationsProvider>();
    }
}
