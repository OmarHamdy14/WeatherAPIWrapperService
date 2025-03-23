using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAPIWrapperService.Data.DTOs;
using WeatherAPIWrapperService.Data.Enums;
using WeatherAPIWrapperService.Data.Services.Interfaces;

namespace WeatherAPIWrapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        [HttpGet("GetWeatherByDynamicDate/{city}/{DynamicDate}")]
        public async Task<IActionResult> GetWeatherByDynamicDate(string city, string DynamicDate)
        {
            if (string.IsNullOrEmpty(city)) return BadRequest(new { Message = "You should assign a city." });

            var res = await _weatherService.GetWeatherByDynamicDate(city,DynamicDate);
            return Ok(res);
        }
        [HttpGet("GetWeatherOfSpecificDate/{city}/{date}")]
        public async Task<IActionResult> GetWeatherOfSpecificDate(string city, DateOnly date)
        {
            if (string.IsNullOrEmpty(city)) return BadRequest(new { Message = "You should assign a city." });
            if (date == null) return BadRequest(new { Message = "Invalid date." });

            var res = await _weatherService.GetWeatherOfSpecificDate(city, date);
            return Ok(res);
        }
        [HttpGet("GetWeatherWithDateRange")]
        public async Task<IActionResult> GetWeatherWithDateRange(WeatherWithDateRangeDTO model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.city)) return BadRequest(new { Message = "You should assign a city." });
                if (model.startDate == null || model.endDate == null) return BadRequest(new { Message = "Invalid startDate or endDate." });
                if (model.startDate > model.endDate) return BadRequest(new { Message = "The start date must be earlier than the end date." });

                var res = await _weatherService.GetWeatherWithDateRange(model.city, model.startDate, model.endDate);
                return Ok(res);
            }
            else return BadRequest(ModelState);
        }
        [HttpGet("GetWeatherDateRangeTypees")]
        public async Task<IActionResult> GetWeatherDateRangeTypees()
        {
            return Ok(DynamicDateTypes.res);
        }
    }
}