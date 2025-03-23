using System.Text.Json;
using WeatherAPIWrapperService.Data.DTOs;
using WeatherAPIWrapperService.Data.Services.Interfaces;

namespace WeatherAPIWrapperService.Data.Services.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiKey = "LGLF86F9QN5D88P89Z7SY94Q6";
        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetWeatherOfSpecificDate(string city,DateOnly date)
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = await client.GetAsync($"{city}/{date}?unitGroup=us&key={apiKey}&contentType=json");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();  
                //var weatherResponse = JsonSerializer.Deserialize<WeatherResponseDTO>(json);
                return json;
            }
            return null;
        }
        public async Task<string> GetWeatherWithDateRange(string city,DateOnly startDate,DateOnly endDate)
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var resp = await client.GetAsync($"{city}/{startDate}/{endDate}?unitGroup=us&key={apiKey}&contentType=json");
            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                //var weatherResponse = JsonSerializer.Deserialize<List<WeatherResponseDTO>>(content);
                return content;
            }
            return null;
        }
        public async Task<string> GetWeatherByDynamicDate(string city,string DynamicDateType)
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = new HttpResponseMessage();
            if (DynamicDateType != "fifteendayforecast") response = await client.GetAsync($"{city}/{DynamicDateType}?unitGroup=us&key={apiKey}&contentType=json");
            else response = await client.GetAsync($"{city}/{DynamicDateType}?unitGroup=us&key={apiKey}&contentType=json");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                //var weatherResponse = JsonSerializer.Deserialize<List<WeatherResponseDTO>>(json);
                return json;
            }
            return null;
        }
    }
}