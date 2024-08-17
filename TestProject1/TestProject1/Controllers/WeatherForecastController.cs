using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            if (rng.Next(1, 100) > 80)
            {
                throw new UnauthorizedAccessException("Unauthorized.");
            }
            else
            if (rng.Next(1, 100) < 20)
            {
                if (rng.Next(1, 100) < 50)
                {
                    throw new ServicesException(101, $"现在是北京时间:{DateTime.UtcNow.AddHours(8):yyyy-MM-dd HH:mm:ss},发生一个服务异常。");
                }
                else
                {
                    throw new Exception($"现在是北京时间:{DateTime.UtcNow.AddHours(8):yyyy-MM-dd HH:mm:ss},发生了一个意想不到的错误。");
                }
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.UtcNow.AddHours(8).AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
        }


        [HttpPost]
        public void Post([FromBody] WeatherForecastRequest request)
        {
            var rng = new Random();
            if (rng.Next(1, 100) >80)
            {
                throw new UnauthorizedAccessException("Unauthorized.");
            }
            else if (rng.Next(1, 100) < 20)
            {
                if (rng.Next(1, 100) < 50)
                {
                    throw new ServicesException(101, $"现在是北京时间:{DateTime.UtcNow.AddHours(8):yyyy-MM-dd HH:mm:ss},发生一个服务异常。");
                }
                else
                {
                    throw new Exception($"现在是北京时间:{DateTime.UtcNow.AddHours(8):yyyy-MM-dd HH:mm:ss},发生了一个意想不到的错误。");
                }
            }
        }


        [HttpPost("method2")]
        public void Method2([FromBody] WeatherForecastRequest request)
        {
            var rng = new Random();
            if (rng.Next(1, 100) > 80)
            {
                throw new UnauthorizedAccessException("Unauthorized.");
            }
            else if (rng.Next(1, 100) < 20)
            {
                if (rng.Next(1, 100) < 50)
                {
                    throw new ServicesException(101, $"现在是北京时间:{DateTime.UtcNow.AddHours(8):yyyy-MM-dd HH:mm:ss},发生一个服务异常。");
                }
                else
                {
                    throw new Exception($"现在是北京时间:{DateTime.UtcNow.AddHours(8):yyyy-MM-dd HH:mm:ss},发生了一个意想不到的错误。");
                }
            }
        }

    }
}
