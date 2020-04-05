using ENews.Data.Models;
using ENews.Services.Mapping;
using System;

namespace ENews.Web.ViewModels
{
    public class ArticleViewModel : IMapFrom<Article>
    {
        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public string SubCategoryTitle { get; set; }

        public string PictureImageUrl { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ContetPreview => this.Content.Substring(0, (this.Content.Length > 100) ? 100 : this.Content.Length);
    }
}