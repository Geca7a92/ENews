using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENews.Services.Mapping;


namespace ENews.Services.Data
{
    public class GalleriesService : IGalleriesService
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Gallery> galleryRepository;

        public GalleriesService(
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
                    Description = imageForGallery.FileName,
                };

                gallery.Images.Add(image);
            }

            await this.galleryRepository.AddAsync(gallery);
            await this.galleryRepository.SaveChangesAsync();

            return gallery.Id;
        }

        public IEnumerable<T> GetLatestGalleries<T>(int skip)
        {
            var model = this.galleryRepository.All().OrderByDescending(g => g.CreatedOn).Where(g => g.Article != null).Skip(skip).To<T>().ToList();
            return model;
        }

        public T GetNewestGallery<T>()
        {
            var model = this.galleryRepository.All().OrderByDescending(g => g.CreatedOn).To<T>().FirstOrDefault();
            return model;
        }

        public T PreviewGalleryById<T>(int id)
        {
            var model = this.galleryRepository.All().Where(g => g.Id == id).To<T>().FirstOrDefault();
            return model;
        }
    }
}
