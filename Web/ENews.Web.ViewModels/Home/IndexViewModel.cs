namespace ENews.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public ArticlePreviewViewModel LatestArticle { get; set; }

        public IEnumerable<ArticlePreviewViewModel> LatestTwoArticles { get; set; }

        public IEnumerable<ArticlePreviewViewModel> LatestPopularNews { get; set; }

        public IEnumerable<ArticlePreviewViewModel> LatestWorldNews { get; set; }

        public IEnumerable<ArticlesVideoPreview> LatestVideos { get; set; }
    }
}
