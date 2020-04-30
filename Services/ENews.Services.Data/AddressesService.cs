namespace ENews.Services.Data
{
    using System.Linq;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;

    public class AddressesService : IAddressesService
    {
        private readonly IRepository<Address> addressRepository;

        public AddressesService(IRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public Address GetAddresById(int id)
        {
            var address = this.addressRepository.All().FirstOrDefault(a => a.Id == id);
            return address;
        }
    }
}
