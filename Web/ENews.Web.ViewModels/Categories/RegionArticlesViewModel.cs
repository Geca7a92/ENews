namespace ENews.Web.ViewModels.Categories
{
    using ENews.Data.Models.Enums;
    using System.Collections.Generic;

    public class RegionArticlesViewModel
    {
        public Region Region { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
