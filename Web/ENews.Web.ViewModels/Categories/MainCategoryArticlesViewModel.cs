using ENews.Data.Models;
using ENews.Services.Mapping;
using ENews.Web.ViewModels.Shared.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Categories
{
    public class MainCategoryArticlesViewModel : PagingViewModel, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ArticlePreviewViewModel> CategoryArticles { get; set; }
    }
}
