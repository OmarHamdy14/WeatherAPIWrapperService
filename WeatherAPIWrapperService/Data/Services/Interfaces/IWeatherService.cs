using WeatherAPIWrapperService.Data.DTOs;
using WeatherAPIWrapperService.Data.Services.Implementation;

namespace WeatherAPIWrapperService.Data.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherOfSpecificDate(string city, DateOnly date);
        Task<string> GetWeatherWithDateRange(string city, DateOnly startDate, DateOnly endDate);
        Task<string> GetWeatherByDynamicDate(string city, string DynamicDateType);
    }
}
