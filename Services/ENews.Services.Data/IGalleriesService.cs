namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ENews.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IGalleriesService
    {
        Task<int> CreateAsync(ICollection<IFormFile> imageCollection, Image mainImage);

        T PreviewGalleryById<T>(int id);

        T GetNewestGallery<T>();

        IEnumerable<T> GetLatestGalleries<T>(int skip);
    }
}
