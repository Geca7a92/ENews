namespace ENews.Services.Data
{
    using System.Threading.Tasks;

    public interface IWeatherService
    {
        Task<T> GetData<T>(string cityName);

        Task<string> GetCityName(string latitude, string longitude);
    }
}
