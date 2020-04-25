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
    public class LatesFiveArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public LatesFiveArticlesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new IndexViewModel
            {
                LatestFiveArticles = this.articleService.GetLatesByCreatedOn<ArticlePreviewViewModel>(5),
            };

            return this.View(model);
        }
    }
}
