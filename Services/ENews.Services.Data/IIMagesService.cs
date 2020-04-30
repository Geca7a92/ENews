namespace ENews.Services.Data
{
    using System.Threading.Tasks;

    using ENews.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        public Task<Image> CreateAsync(IFormFile formFile);

        public Image GetImageById(int id);
    }
}
