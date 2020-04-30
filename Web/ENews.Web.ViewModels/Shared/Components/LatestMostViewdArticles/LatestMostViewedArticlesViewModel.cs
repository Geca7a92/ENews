namespace ENews.Web.ViewModels.Shared.Components.LatestMostViewdArticles
{
    using System.Collections.Generic;

    public class LatestMostViewedArticlesViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> Articles { get; set; }
    }
}
