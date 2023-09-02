namespace IPLocations.Api;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddLocationsCache(this IServiceCollection services)
    {
        services.AddOptions<CacheOptions>()
            .BindConfiguration("Caching")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddMemoryCache()
            .AddSingleton<ILocationsCache, InMemoryLocationsCache>();

        return services;
    }
}
