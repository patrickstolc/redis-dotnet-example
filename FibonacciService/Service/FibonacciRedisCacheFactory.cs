namespace FibonacciService.Service;

public static class FibonacciRedisCacheFactory
{
    public static FibonacciRedisCache Create()
    {
        return new FibonacciRedisCache(
            "redis",
            6379,
            ""
        );
    }
}