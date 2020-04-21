namespace ENews.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    public class RegionArticlesViewModel
    {
        public string RegionName { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
