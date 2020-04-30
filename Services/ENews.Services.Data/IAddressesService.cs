namespace ENews.Services.Data
{
    using ENews.Data.Models;
    using System.Threading.Tasks;

    public interface IAddressesService
    {
        public Address GetAddresById(int id);
    }
}
