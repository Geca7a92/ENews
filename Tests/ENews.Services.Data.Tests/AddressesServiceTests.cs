namespace ENews.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;
    using ENews.Data;
    using ENews.Data.Models;
    using ENews.Data.Repositories;
    using ENews.Services.Data;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Services.Data;
    using Xunit;
    public class AddressesServiceTests
    {
        [Fact]
        public async Task TestGetById()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var moqSe = new Mock<ISettingsService>();

            var repository = new EfRepository<Address>(new ApplicationDbContext(options));
            var service = new AddressesService(repository, moqSe.Object);

            var addres = new Address()
            {
                Id = 1,
            };
            await repository.AddAsync(addres);
            await repository.SaveChangesAsync();
            var res = service.GetAddresById(1);
            Assert.Equal(addres, res);
        }
    }
}
