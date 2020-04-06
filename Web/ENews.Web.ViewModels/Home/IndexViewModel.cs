using ENews.Web.ViewModels.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> LatestFiveArticles { get; set; }
    }
}
