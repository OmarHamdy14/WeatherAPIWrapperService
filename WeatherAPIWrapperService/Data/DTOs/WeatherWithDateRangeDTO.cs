using System.ComponentModel.DataAnnotations;

namespace WeatherAPIWrapperService.Data.DTOs
{
    public class WeatherWithDateRangeDTO
    {
        [Required]
        public string city { get; set; }
        [Required]
        public DateOnly startDate { get; set; }
        [Required]
        public DateOnly endDate { get; set; }

    }
}
