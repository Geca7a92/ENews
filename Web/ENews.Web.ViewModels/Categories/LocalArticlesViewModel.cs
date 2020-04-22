using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Categories
{
    public class LocalArticlesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
