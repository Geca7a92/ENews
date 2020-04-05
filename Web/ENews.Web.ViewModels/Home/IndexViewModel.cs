using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<ArticleViewModel> LatestFiveArticles { get; set; }
    }
}
