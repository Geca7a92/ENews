using ENews.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IGalleriesService
    {
        Task<int> CreateAsync(ICollection<IFormFile> imageCollection, Image mainImage);

        T PreviewGalleryById<T>(int id);
    }
}
