namespace ENews.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using Microsoft.AspNetCore.Http;

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
