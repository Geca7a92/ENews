using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using System.Linq;

namespace ENews.Services.Data
{
    public class AddressesService : IAddressesService
    {
        private readonly IRepository<Address> addressRepository;
        private readonly ISettingsService settingsService;

        public AddressesService(IRepository<Address> addressRepository, ISettingsService settingsService)
        {
            this.addressRepository = addressRepository;
            this.settingsService = settingsService;
        }

        public Address GetAddresById(int id)
        {
            var address = this.addressRepository.All().FirstOrDefault(a => a.Id == id);
            return address;
        }
    }
}
