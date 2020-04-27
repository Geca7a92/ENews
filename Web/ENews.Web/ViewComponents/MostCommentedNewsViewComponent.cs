using ENews.Services.Data;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.Home;
using ENews.Web.ViewModels.Shared.Components.MostCommentedNews;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.ViewComponents
{
    public class MostCommentedNewsViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public MostCommentedNewsViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new MostCommentedNewsViewModel
            {
                Articles = this.articleService.GetLatesMostCommented<ArticlePreviewViewModel>(5),
            };

            return this.View(model);
        }
    }
}
