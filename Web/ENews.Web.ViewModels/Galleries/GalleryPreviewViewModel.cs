using ENews.Data.Models;
using ENews.Services.Mapping;
using ENews.Web.ViewModels.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Galleries
{
    public class GalleryPreviewViewModel : IMapFrom<Gallery>
    {
        public IEnumerable<ImagePreviewViewModel> Images { get; set; }

        public int ArticleId { get; set; }
    }
}
