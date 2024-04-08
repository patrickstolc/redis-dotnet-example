namespace AnalyticsService.Client;

public static class MyRedisClientFactory
{
    public static MyRedisClient CreateClient()
    {
        return new MyRedisClient("redis", 6379, "");
    }
}