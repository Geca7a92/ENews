using ENews.Services.Data;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.Home;
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

        public IViewComponentResult Invoke()
        {
            var model = new IndexViewModel
            {
                LatestTwoArticles = this.articleService.GetLatesMostViewed<ArticlePreviewViewModel>(2),
            };

            return this.View(model);
        }
    }
}
