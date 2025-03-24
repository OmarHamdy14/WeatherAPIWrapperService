using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using WeatherAPIWrapperService.Data.Services.Interfaces;

namespace WeatherAPIWrapperService.Data.Services.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        public WeatherService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }
        public async Task<string> GetWeatherOfSpecificDate(string city,DateOnly date)
        {
            var data = _memoryCache.Get<string>($"WeatherOf{date}For{city}");
            if(data!=null) return data;

            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = await client.GetAsync($"{city}/{date}?unitGroup=us&key={_configuration["WeatherAPIKeys:visualcrossingAPIKey"]}&contentType=json");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(); 
                _memoryCache.Set($"WeatherOf{date}For{city}", json,DateTimeOffset.Now.AddMinutes(5));
                //var weatherResponse = JsonSerializer.Deserialize<WeatherResponseDTO>(json);
                return json;
            }
            return null;
        }
        public async Task<string> GetWeatherWithDateRange(string city,DateOnly startDate,DateOnly endDate)
        {
            var data = _memoryCache.Get<string>($"WeatherOf{startDate}to{endDate}For{city}");
            if (data != null) return data;

            var client = _httpClientFactory.CreateClient("WeatherClient");
            var resp = await client.GetAsync($"{city}/{startDate}/{endDate}?unitGroup=us&key={_configuration["WeatherAPIKeys:visualcrossingAPIKey"]}&contentType=json");
            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                _memoryCache.Set($"WeatherOf{startDate}to{endDate}For{city}", json, DateTimeOffset.Now.AddMinutes(5));
                //var weatherResponse = JsonSerializer.Deserialize<List<WeatherResponseDTO>>(content);
                return json;
            }
            return null;
        }
        public async Task<string> GetWeatherByDynamicDate(string city,string DynamicDateType)
        {
            var data = _memoryCache.Get<string>($"WeatherOfSpecificDate{DynamicDateType}For{city}");
            if (data != null) return data;

            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = new HttpResponseMessage();
            if (DynamicDateType != "fifteendayforecast") response = await client.GetAsync($"{city}/{DynamicDateType}?unitGroup=us&key={_configuration["WeatherAPIKeys:visualcrossingAPIKey"]}&contentType=json");
            else response = await client.GetAsync($"{city}/{DynamicDateType}?unitGroup=us&key={_configuration["WeatherAPIKeys:visualcrossingAPIKey"]}&contentType=json");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                _memoryCache.Set($"WeatherOfSpecificDate{DynamicDateType}For{city}", json, DateTimeOffset.Now.AddMinutes(5));
                //var weatherResponse = JsonSerializer.Deserialize<List<WeatherResponseDTO>>(json);
                return json;
            }
            return null;
        }
    }
}