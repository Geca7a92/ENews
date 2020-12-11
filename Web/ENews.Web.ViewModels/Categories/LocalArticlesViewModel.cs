namespace ENews.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using ENews.Web.ViewModels.Shared.Paging;

    public class LocalArticlesViewModel : ArticlesPagingViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
