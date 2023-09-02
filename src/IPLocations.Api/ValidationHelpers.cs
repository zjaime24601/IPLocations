using System.Net;

namespace IPLocations.Api;

public static class ValidationHelpers
{
    public static bool IsValidIPAddress(string ipAddress)
        => IPAddress.TryParse(ipAddress, out var _);
}
