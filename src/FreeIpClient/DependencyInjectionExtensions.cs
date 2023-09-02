using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FreeIpClient;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddFreeIpClient(this IServiceCollection services)
    {
        services.AddOptions<FreeIpClientOptions>()
            .BindConfiguration("FreeIp");

        services.AddHttpClient<IFreeIpClient, HttpFreeIpClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<FreeIpClientOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });
        return services;
    }
}
