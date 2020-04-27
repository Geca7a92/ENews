using ENews.Web.ViewModels.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public ArticlePreviewViewModel LatestArticle { get; set; }

        public IEnumerable<ArticlePreviewViewModel> LatestTwoArticles { get; set; }

        public IEnumerable<ArticlePreviewViewModel> LatestFiveArticles { get; set; }

        public IEnumerable<ArticlePreviewViewModel> LatestPopularNews { get; set; }

        public IEnumerable<ArticlePreviewViewModel> MostCommentedNewsLastWeek { get; set; }

        public IEnumerable<ArticlePreviewViewModel> LatestWorldNews { get; set; }
    }
}
