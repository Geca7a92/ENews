using ENews.Services.Data;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.Home;
using ENews.Web.ViewModels.Shared.Components.LatestMostViewdArticles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.ViewComponents
{
    public class LatestMostViewedArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public LatestMostViewedArticlesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke(int count, int skip = 0)
        {
            var model = new LatestMostViewedArticlesViewModel
            {
                Articles = this.articleService.GetLatesMostViewed<ArticlePreviewViewModel>(count, skip),
            };
            return this.View(model);
        }
    }
}
