namespace ENews.Web.ViewModels.MembersArea.Home
{
    using System.Collections.Generic;

    public class IndexArticlesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexArticleViewModel> Articles { get; set; }

    }
}
