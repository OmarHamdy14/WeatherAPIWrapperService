namespace WeatherAPIWrapperService.Data.Enums
{
    public static class DynamicDateTypes
    {
        public enum Types
        {
            fifteendayforecast,
            today,
            yesterday,
            tomorrow,
            yeartodate,
            monthtodate,
            last7days,
            next7days,
            last24hours,
            next24hours
        }
        public static readonly List<string> res = Enum.GetNames(typeof(Types)).ToList();
    }
}