using FibonacciService.Service;
using Microsoft.AspNetCore.Mvc;

namespace FibonacciService.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciServiceController : ControllerBase
{
    private readonly FibonacciRedisCache _fibonacciRedisCache;
    
    public FibonacciServiceController(FibonacciRedisCache fibonacciRedisCache)
    {
        _fibonacciRedisCache = fibonacciRedisCache;
        _fibonacciRedisCache.Connect();
    }

    [HttpGet("{n}")]
    public ActionResult<long> Get(int n)
    {
        if (n < 0 || n > 92)
        {
            return BadRequest("Please provider a number between 0 and 92");
        }

        var result = _fibonacciRedisCache.Calculate(n);
        return Ok(result);
    }
}