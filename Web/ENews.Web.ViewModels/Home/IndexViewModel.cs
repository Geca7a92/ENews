namespace ENews.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using ENews.Web.ViewModels.Articles;

    public class IndexViewModel
    {
        public ArticlePreviewViewModel LatestArticle { get; set; }

        public IEnumerable<ArticleBaseViewModel> LatestTwoArticles { get; set; }

        public IEnumerable<ArticleBaseViewModel> LatestPopularNews { get; set; }

        public IEnumerable<ArticleBaseViewModel> LatestWorldNews { get; set; }

        public IEnumerable<ArticlesVideoPreview> LatestVideos { get; set; }
    }
}
