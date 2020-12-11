namespace ENews.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using ENews.Data.Models.Enums;
    using ENews.Web.ViewModels.Shared.Paging;

    public class RegionArticlesViewModel : ArticlesPagingViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
