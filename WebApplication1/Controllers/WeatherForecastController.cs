using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProfileSharp.Attributes;

namespace WebApplication1.Controllers
{
    [EnableProfileSharp]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IWeatherRepository _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherRepository weatherRepository)
        {
            _logger = logger;
            _repository = weatherRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.Get());
        }

        [HttpGet("range/{days}")]
        public IActionResult GetRange(int days)
        {
            try
            {
                var weather = _repository.GetRange(days);

                return Ok(weather);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
