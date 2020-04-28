using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Home
{
    public class ArticlesVideoPreview : IMapFrom<Article>
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string PictureImageUrl { get; set; }
    }
}
