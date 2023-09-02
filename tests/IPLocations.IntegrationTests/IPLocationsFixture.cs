using FreeIpClient;
using IPLocations.Api.Storage.Mongo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IPLocations.IntegrationTests;

public class IPLocationsFixture : WebApplicationFactory<Program>
{
    public IMongoCollection<LocationDocument> GetLocationsStorage()
    {
        return Services.GetRequiredService<IMongoDatabase>()
            .GetCollection<LocationDocument>("ipLocations");
    }

    public async Task ClearLocationsStorage()
    {
        await Services.GetRequiredService<IMongoDatabase>()
            .DropCollectionAsync("ipLocations");
    }

    public void ClearCache()
    {
        var cache = Services.GetRequiredService<IMemoryCache>() as MemoryCache;
        cache.Clear();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureAppConfiguration(config =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile("appsettings.Test.json");
        });
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IFreeIpClient, FreeIpStubClient>();
        });
    }
}

[CollectionDefinition(Name)]
public class IPLocationsApiCollection : ICollectionFixture<IPLocationsFixture>
{
    public const string Name = nameof(IPLocationsApiCollection);
}
