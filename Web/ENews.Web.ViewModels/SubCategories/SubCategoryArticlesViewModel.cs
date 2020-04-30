namespace ENews.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class SubCategoryArticlesViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ArticlePreviewViewModel> SubCategoryArticles { get; set; }
    }
}
