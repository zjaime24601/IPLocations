using System.Net.Http.Json;

namespace FreeIpClient;

public class HttpFreeIpClient : IFreeIpClient
{
    private readonly HttpClient _client;

    public HttpFreeIpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IpLocation> LookupIpLocation(string ipAddress)
    {
        try
        {
            // TODO implement retry policy
            var response = await _client.GetAsync($"api/json/{ipAddress}");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IpLocation>();
        }
        catch (Exception ex)
        {
            throw new FreeIpException("Failed to lookup IP address from FreeIP", ex);
        }
    }
}
