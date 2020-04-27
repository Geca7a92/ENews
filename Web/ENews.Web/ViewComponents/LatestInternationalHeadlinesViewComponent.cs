using ENews.Services.Data;
using ENews.Web.ViewModels.Shared.Components.LatestHeadlines;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.ViewComponents
{
    public class LatestInternationalHeadlinesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public LatestInternationalHeadlinesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new LatestHeadlinesViewModel();

            model.Articles = this.articleService.GetLatesInternationalArticles<LatestHeadlineViewModel>(3);

            return this.View(model);
        }
    }
}
