using AnalyticsService.Client;
using AnalyticsService.Schema;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalyticsServiceController : ControllerBase
{
    private readonly MyRedisClient _redisClient;
    
    public AnalyticsServiceController(MyRedisClient redisClient)
    {
        _redisClient = redisClient;
        _redisClient.Connect();
    }
    
    [HttpPost]
    public void SaveAnalyticsData([FromBody]AnalyticsRequest request)
    {
        _redisClient.StoreString(request.ClientId, request.Payload);
    }
    
    [HttpGet]
    public string? ReadAnalyticsData(string clientId)
    {
        return _redisClient.GetString(clientId);
    }
    
    [HttpDelete]
    public void DeleteAnalyticsData([FromBody]AnalyticsDeleteRequest request)
    {
        _redisClient.RemoveString(request.ClientId);
    }
}