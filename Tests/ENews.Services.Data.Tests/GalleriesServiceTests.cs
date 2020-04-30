using ENews.Services.Data.Tests.Repositories;
using ENews.Services.Data.Tests.Seed;
using ENews.Services.Mapping;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ENews.Services.Data.Tests
{
    public class GalleriesServiceTests
    {
        //[Fact]
        //public void PreviewGalleryById()
        //{
        //    var repository = GalleryRepository.Create();
        //    var cloudinaryMoq = new Mock<ICloudinaryService>();

        //    var gallery = SeedGallery.Create();

        //    repository.AddAsync(gallery).GetAwaiter().GetResult();
        //    repository.SaveChangesAsync().GetAwaiter().GetResult();

        //    var service = new GalleriesService(cloudinaryMoq.Object, repository);

        //    AutoMapperConfig.RegisterMappings(typeof(CreateCommentInputModel).GetTypeInfo().Assembly);

        //    service.CreateAsync(new CreateCommentInputModel { Content = "New comment" }).GetAwaiter();

        //    Assert.Equal(2, repository.All().Count());
        //}



        //Task<int> CreateAsync(ICollection<IFormFile> imageCollection, Image mainImage);

        //T PreviewGalleryById<T>(int id);

        //T GetNewestGallery<T>();

        //IEnumerable<T> GetLatestGalleries<T>(int skip);
    }
}
