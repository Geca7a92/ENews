namespace ENews.Web.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using ENews.Services.Data;
    using ENews.Web.ViewModels.Weather;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class WeatherController : Controller
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public async Task<IActionResult> GetData()
        {
            var ip = this.GetUserIP();
            var cityName = this.weatherService.GetUserCityByIp(ip);
            var model = await this.weatherService.GetData<WeatherModel>(cityName);
            model.City = cityName;
            return this.Json(model);
        }

        private string GetUserIP()
        {
            return this.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
