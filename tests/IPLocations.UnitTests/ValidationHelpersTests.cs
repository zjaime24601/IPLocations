using FluentAssertions;
using IPLocations.Api;

namespace IPLocations.UnitTests;

public class ValidationHelpersTests
{
    public class IsValidIPAddress
    {
        [Theory]
        [InlineData("127.0.0.1")]
        [InlineData("::")]
        [InlineData("2001:db8:3333:4444:5555:6666:7777:8888")]
        [InlineData("2001:db8::")]
        [InlineData("::1234:5678")]
        [InlineData("2001:db8::1234:5678")]
        public void GivenValidIPAddress_ThenReturnsTrue(string ipAddress)
        {
            ValidationHelpers.IsValidIPAddress(ipAddress).Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Jibberish")]
        [InlineData("127.0.0.")]
        [InlineData("127.0.0.1.")]
        [InlineData("256.0.0.1")]
        public void GivenInvalidIPAddress_ThenReturnsFalse(string ipAddress)
        {
            ValidationHelpers.IsValidIPAddress(ipAddress).Should().BeFalse();
        }
    }
}
