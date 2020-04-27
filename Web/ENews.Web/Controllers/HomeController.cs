namespace ENews.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IArticlesService articleService;

        public HomeController(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                LatestArticle = this.articleService.GetLastByCreatedOn<ArticlePreviewViewModel>(),
                LatestTwoArticles = this.articleService.GetLatesByCreatedOn<ArticlePreviewViewModel>(2, 1),
                LatestFiveArticles = this.articleService.GetLatesByCreatedOn<ArticlePreviewViewModel>(5, 3),
                LatestPopularNews = this.articleService.GetLatesMostViewed<ArticlePreviewViewModel>(4),
                LatestWorldNews = this.articleService.GetLatesInternationalArticles<ArticlePreviewViewModel>(6),
            };
            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
