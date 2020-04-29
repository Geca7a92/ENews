using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.MembersArea.Home
{
    public class IndexArticlesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexArticleViewModel> Articles { get; set; }

    }
}
