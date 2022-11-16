using DoINeedUmbrellaToday.Services;
using DoINeedUmbrellaToday.Weather;
using Microsoft.AspNetCore.Mvc;

namespace DoINeedUmbrellaToday.Controllers
{
    [Route("api/forecast")]
    [ApiController]
    public class ForecastController : ControllerBase
    {

        private readonly ILogger<ForecastController> _logger;

        private readonly IConfiguration _config;

        public ForecastController(ILogger<ForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetForecast([FromQuery] UserLocation location, [FromServices] IForecastService forecast)
        {
            try
            {
                var forecastResult = await forecast.GetForecastForToday(location);
                return Ok(forecastResult.WeatherCode);
            }
            catch (InvalidLocationException e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
        }

    }
}
