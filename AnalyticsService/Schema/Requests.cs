namespace AnalyticsService.Schema;

public class AnalyticsRequest
{
    public string ClientId { get; set; }
    public string Payload { get; set; }
}

public class AnalyticsDeleteRequest
{
    public string ClientId { get; set; }
}