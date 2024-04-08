using StackExchange.Redis;

namespace AnalyticsService.Client;

public class MyRedisClient
{
    private readonly string _hostname;
    private readonly int _port;
    private readonly string _password;

    private ConnectionMultiplexer redis;
    
    public MyRedisClient(string hostname, int port, string password)
    {
        _hostname = hostname;
        _port = port;
        _password = password;
    }

    public void Connect()
    {
        string connectionString = $"{_hostname}:{_port},password={_password}";
        redis = ConnectionMultiplexer.Connect(connectionString);
    }
    
    public IDatabase GetDatabase()
    {
        return redis.GetDatabase();
    }

    public void StoreString(string key, string value)
    {
        var db = GetDatabase();
        db.StringSet(key, value);
    }

    public string? GetString(string key)
    {
        var db = GetDatabase();
        return db.StringGet(new RedisKey(key));
    }

    public void RemoveString(string key)
    {
        var db = GetDatabase();
        db.KeyDelete(key);
    }
}