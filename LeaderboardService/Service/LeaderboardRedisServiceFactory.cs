namespace LeaderboardService.Service;

public static class LeaderboardRedisServiceFactory
{
    public static LeaderboardRedisService Create()
    {
        return new LeaderboardRedisService(
            "redis",
            6379,
            ""
        );
    }
}