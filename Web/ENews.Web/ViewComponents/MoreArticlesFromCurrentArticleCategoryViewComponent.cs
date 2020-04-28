using ENews.Services.Data;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.Shared.Components.MoreArticlesFromCurrentArticleCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.ViewComponents
{
    public class MoreArticlesFromCurrentArticleCategoryViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public MoreArticlesFromCurrentArticleCategoryViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke(int categoryId, int take, int articleId, int skip = 0)
        {
            var model = new MoreArticlesFromCurrentArticleCategoryViewModel
            {
                Articles = this.articleService.GetAllByCategoryId<ArticlePreviewViewModel>(categoryId, take, skip, articleId),
            };

            return this.View(model);
        }
    }
}
