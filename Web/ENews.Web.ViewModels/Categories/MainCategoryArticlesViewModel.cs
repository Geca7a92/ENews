using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Categories
{
    public class MainCategoryArticlesViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}
