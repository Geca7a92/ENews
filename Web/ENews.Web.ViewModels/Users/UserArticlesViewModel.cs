namespace ENews.Web.ViewModels.Users
{
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Shared.Paging;

    public class UserArticlesViewModel : ArticlesPagingViewModel, IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public IEnumerable<ArticlePreviewViewModel> UserArticles { get; set; }
    }
}
