namespace ENews.Web.ViewModels.Shared.Components.LatestArticles
{
    using System.Collections.Generic;

    public class LatestArticlesViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> Articles { get; set; }
    }
}
