namespace ENews.Services.Data
{
    using ENews.Data.Models;

    public interface IAddressesService
    {
        public Address GetAddresById(int id);
    }
}
