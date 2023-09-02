namespace FreeIpClient;

public class IpLocation
{
    public int IpVersion { get; set; }

    public string IpAddress { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string CountryName { get; set; }

    public string CountryCode { get; set; }

    public string TimeZone { get; set; }

    public string ZipCode { get; set; }

    public string CityName { get; set; }

    public string RegionName { get; set; }

    public string Continent { get; set; }

    public string ContinentCode { get; set; }
}
