namespace ENews.Web.ViewModels.Weather
{
    public class WeatherModel
    {
        public string City { get; set; }

        public Weather[] Weather { get; set; }

        public Main Main { get; set; }

        public Wind Wind { get; set; }

        public double Visibility { get; set; }
    }
}
