using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Images
{
    public class ImagePreviewViewModel : IMapFrom<Image>
    {
        public string ImageUrl { get; set; }
    }
}
