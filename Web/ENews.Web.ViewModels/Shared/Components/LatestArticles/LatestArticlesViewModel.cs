using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Shared.Components.LatestArticles
{
    public class LatestArticlesViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> Articles { get; set; }
    }
}
