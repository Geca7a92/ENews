namespace ENews.Services.Data
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ENews.Web.ViewModels.Weather;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration configuration;

        public WeatherService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> GetCityName(string latitude, string longitude)
        {
            HttpClient client = new HttpClient();
            var googleApi = this.configuration["Googleapis:GeolocateApi"];

            var url = string.Format(@"https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}", latitude, longitude, googleApi);
            HttpResponseMessage response = await client.GetAsync(url);
            var str = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(str);
            var code = json["plus_code"];
            var cityname = code["compound_code"].ToString().Split(' ')[1].TrimEnd(',');
            return cityname;
        }

        public async Task<T> GetData<T>(string cityName)
        {
            var openWeatherApi = this.configuration["OpenWeather:Api"];
            string baseUrlCityName = string.Format(@"http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}", cityName, openWeatherApi);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(baseUrlCityName);
            var str = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject(str);
            T model = JsonConvert.DeserializeObject<T>(str);
            return model;
        }

        public string GetUserCityByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
            }
            catch (Exception)
            {
                ipInfo.City = null;
            }

            return ipInfo.City;
        }
    }
}
