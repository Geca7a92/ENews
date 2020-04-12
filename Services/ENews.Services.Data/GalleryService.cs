using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public class GalleryService : IGalleryService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Gallery> galleryRepository;

        public GalleryService(
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<Gallery> galleryRepository)
        {
            this.cloudinaryService = cloudinaryService;
            this.galleryRepository = galleryRepository;
        }

        public async Task<int> CreateAsync(ICollection<IFormFile> imageCollection, Image mainImage)
        {
            var gallery = new Gallery();

            gallery.Images.Add(mainImage);

            foreach (var imageForGallery in imageCollection)
            {
                var imageUrl = await this.cloudinaryService.UploadPictureAsync(imageForGallery);
                var image = new Image()
                {
                    ImageUrl = imageUrl,
                    Description = imageUrl,
                };

                gallery.Images.Add(image);
            }

            await this.galleryRepository.AddAsync(gallery);
            await this.galleryRepository.SaveChangesAsync();

            return gallery.Id;
        }

        public T PreviewGalleryById<T>(int id)
        {
            var model = this.galleryRepository.All().Where(g => g.Id == id).To<T>().FirstOrDefault();
            return model;
        }
    }
}
