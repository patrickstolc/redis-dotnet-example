using LeaderboardService.Service;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardService.Controllers;

[ApiController]
[Route("[controller]")]
public class LeaderboardServiceController : ControllerBase
{
    private readonly LeaderboardRedisService _leaderboardService;
    
    public LeaderboardServiceController(LeaderboardRedisService leaderboardService)
    {
        _leaderboardService = leaderboardService;
        _leaderboardService.Connect();
    }

    [HttpPost]
    public IActionResult SubmitScore([FromBody] ScoreSubmission submission)
    {
        _leaderboardService.AddOrUpdateScore(submission.Username, submission.Score);
        return Ok();
    }

    [HttpGet("{top}")]
    public ActionResult<IEnumerable<KeyValuePair<string, double>>> GetTopUsers(int top)
    {
        var topUsers = _leaderboardService.GetTopUsers(top);
        return Ok(topUsers);
    }
}

public class ScoreSubmission
{
    public string Username { get; set; }
    public double Score { get; set; }
}