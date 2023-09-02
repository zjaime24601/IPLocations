namespace IPLocations.UnitTests;

public static class SeedDataHelpers
{
    public static double NextLatitude(this Random random)
        => (random.NextDouble() - 0.5) * 90;

    public static double NextLongitude(this Random random)
        => (random.NextDouble() - 0.5) * 180;
}
