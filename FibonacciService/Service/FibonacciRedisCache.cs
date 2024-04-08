using StackExchange.Redis;

namespace FibonacciService.Service;

public class FibonacciRedisCache
{
    private readonly string _hostname;
    private readonly int _port;
    private readonly string _password;
    private ConnectionMultiplexer _redisConnection;

    public FibonacciRedisCache(string hostname, int port, string password)
    {
        _hostname = hostname;
        _port = port;
        _password = password;
    }

    public void Connect()
    {
        _redisConnection = ConnectionMultiplexer.Connect($"{_hostname}:{_port},password={_password}");
    }
    
    public IDatabase GetDatabase()
    {
        return _redisConnection.GetDatabase();
    }

    public long Calculate(int n)
    {
        string cacheKey = $"fib:{n}";
        string cachedValue = GetDatabase().StringGet(cacheKey);

        if (!string.IsNullOrEmpty(cachedValue))
            return long.Parse(cachedValue);
        
        long result = CalculateFibonacci(n);
        GetDatabase().StringSet(cacheKey, result.ToString(), TimeSpan.FromSeconds(30));
        return result;
    }

    private long CalculateFibonacci(int n)
    {
        if (n <= 1) return n;
        return CalculateFibonacci(n - 1) + CalculateFibonacci(n - 2);
    }
}