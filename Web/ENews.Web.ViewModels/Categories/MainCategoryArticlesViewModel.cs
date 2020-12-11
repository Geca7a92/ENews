namespace ENews.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Shared.Paging;

    public class MainCategoryArticlesViewModel : ArticlesPagingViewModel, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
