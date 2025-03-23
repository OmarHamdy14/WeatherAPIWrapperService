namespace WeatherAPIWrapperService.Data.DTOs
{
    public class WeatherResponseDTO
    {
        public string name  { get; set; }
        public string description { get; set; }
        public DateTime datetime { get; set; }
        public float temp { get; set; }
        public float humidity { get; set; }
        public float feelslike { get; set; }
        public float snow { get; set; }
        public float windgust { get; set; }
        public float windspeed { get; set; }
        public float pressure { get; set; }

    }
}
