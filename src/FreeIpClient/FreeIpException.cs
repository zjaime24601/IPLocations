namespace FreeIpClient;

public class FreeIpException : Exception
{
    public FreeIpException(string message)
        : base(message)
    {
    }

    public FreeIpException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
