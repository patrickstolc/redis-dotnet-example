using StackExchange.Redis;

namespace LeaderboardService.Service;

public class LeaderboardRedisService
{
    private readonly string _hostname;
    private readonly int _port;
    private readonly string _password;
    
    private ConnectionMultiplexer _redisConnection;
    
    public LeaderboardRedisService(string hostname, int port, string password)
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

    public void AddOrUpdateScore(string username, double score)
    {
        var db = GetDatabase();
        db.SortedSetAdd("quiz:leaderboard", username, score);
    }

    public IEnumerable<KeyValuePair<string, double>> GetTopUsers(int top = 10)
    {
        var db = GetDatabase();
        var range = db.SortedSetRangeByRankWithScores("quiz:leaderboard", 0, top - 1, Order.Descending);

        return range.Select(x => new KeyValuePair<string, double>(x.Element, x.Score));
    }
}