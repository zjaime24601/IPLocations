using System.ComponentModel.DataAnnotations;

namespace FreeIpClient;

public class FreeIpClientOptions
{
    [Required]
    public string BaseUrl { get; init; }
}
