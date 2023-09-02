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