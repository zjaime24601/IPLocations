using Microsoft.AspNetCore.Mvc.Testing;

namespace IPLocations.IntegrationTests;

public class IPLocationsFixture : WebApplicationFactory<Program>
{

}

[CollectionDefinition(Name)]
public class IPLocationsApiCollection : ICollectionFixture<IPLocationsFixture>
{
    public const string Name = nameof(IPLocationsApiCollection);
}