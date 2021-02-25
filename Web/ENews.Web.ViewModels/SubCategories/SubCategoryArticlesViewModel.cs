namespace ENews.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Shared.Paging;

    public class SubCategoryArticlesViewModel : ArticlesPagingViewModel, IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public IEnumerable<ArticlePreviewViewModel> SubCategoryArticles { get; set; }
    }
}
