using ENews.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IImagesService
    {
        public Task<Image> CreateAsync(IFormFile formFile);

        public Image GetImageById(int id);
    }
}
