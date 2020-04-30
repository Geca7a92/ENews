namespace ENews.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    public class LocalArticlesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
