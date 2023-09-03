# Crezco Practical Task
Build a micro-service that would use any 3rd party location-from-IP address service.

- External Provider - In my case I've opted to use [FreeIP](https://freeipapi.com/)
- Persistence - Uses MongoDb
    - Stores looked up values
    - Provides a fallback when the external provider lookup fails (If we've previously looked up that value successfully before)
- Caching
    - Uses the built in InMemoryCache provided by AspNet.Core. An Improvement here would be to move to a distributed cache.
    - Expiration times are configurable in appsettings.
- Testing - Using xUnit
    - Integration tests primarily used for overarching behaviours.
    - Unit tests mostly used to quickly test behaviours around isolated utilities. All done using xUnit.

### Self-Review
- Test coverage could be higher
    - Could have unit tests covering the FreeIpClient to ensure the test is being well formed.
    - Could use wiremock or a stub HttpMessageHandler in the integration tests instead of stubbing the IFreeApiClient.
    - Could have some unit tests around the DI setup to test different configurations. Though since there isn't much complex logic in the DI setup these tests aren't sorely missed.
- Caching could do with some different configuration in cases where the external api fails and falls back to our persisted values. These values should be cached for less time (if at all).

## To Run
The simplest way to run is to execute the below:
```
docker-compose up -d ip-locations
```
Swagger UI will be available at http://localhost:5100/swagger/index.html

## Tests
To run tests you must have a mongodb instance running. So you must run either the above snippet or:
```
docker-compose up -d mongodb
```
Then you can run `dotnet test` from the solution directory.