using ENews.Data.Models.Enums;
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
    public class GetLocalLastFiveArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public GetLocalLastFiveArticlesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke(Region region)
        {
            //var model = new IndexViewModel
            //{
            //    LatestFiveArticles = this.articleService.GetLatestByRegion<ArticlePreviewViewModel>(region),
            //};

            return this.View();
        }
    }
}
