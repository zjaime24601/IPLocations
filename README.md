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

Swagger UI is available at https://localhost:7232/swagger/index.html

## Dependencies

The included [Docker Compose file](./compose.yml) contains the necessary mongodb instance as configured in appsettings as well as a mongo express service to act as a UI for that instance. 

To spin up only the necessary dependencies run the following in the solution directory:
```
docker-compose up -d mongo
```

You can then run or test the service with the following respective commands in the solution directory:
```
dotnet run --project .\src\IPLocations.Api\IPLocations.Api.csproj

dotnet test
```