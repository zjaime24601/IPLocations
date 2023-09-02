namespace FreeIpClient;

public interface IFreeIpClient
{
    Task<IpLocation> LookupIpLocation(string ipAddress);
}
