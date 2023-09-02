using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

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
        }).AddPolicyHandler(GetRetryPolicy());
        return services;
    }
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(5, retryAttempt
                => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
