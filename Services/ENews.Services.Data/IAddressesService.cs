using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENews.Services.Data
{
    public interface IAddressesService
    {
        public Address GetAddresById(int id);
    }

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
