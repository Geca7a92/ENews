using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IImagesService
    {
        public Task<Image> CreateAsync(IFormFile formFile);

        public Image GetImageById(int id);
    }

    public class ImagesService : IImagesService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ImagesService(
            ICloudinaryService cloudinaryService, 
            IDeletableEntityRepository<Image> imageRepository)
        {
            this.cloudinaryService = cloudinaryService;
            this.imageRepository = imageRepository;
        }

        public async Task<Image> CreateAsync(IFormFile formFile)
        {
            var mainImageUrl = await this.cloudinaryService.UploadPictureAsync(formFile);
            var image = new Image()
            {
                ImageUrl = mainImageUrl,
                Description = "Profile picture",
            };

            await this.imageRepository.AddAsync(image);
            await this.imageRepository.SaveChangesAsync();
            return image;
        }

        public Image GetImageById(int id)
        {
            var image = this.imageRepository.All().FirstOrDefault(i => i.Id == id);
            return image;
        }
    }
}
