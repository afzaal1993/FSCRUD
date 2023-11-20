using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ICapPublisher _capPublisher;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ICapPublisher capPublisher)
    {
        _logger = logger;
        _capPublisher = capPublisher;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<string> Get()
    {
        await _capPublisher.PublishAsync("helloWorld", "CodeOpinion");
        return "1";
    }
}
