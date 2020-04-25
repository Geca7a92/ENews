using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Rss
{
    public class ArticleModel : IMapFrom<Article>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string PictureImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
