namespace ENews.Web.ViewModels.MembersArea.Home
{
    using System.Collections.Generic;

    using ENews.Web.ViewModels.Shared.Paging;

    public class IndexArticlesViewModel : PagingViewModel
    {
        public IEnumerable<IndexArticleViewModel> Articles { get; set; }

    }
}
