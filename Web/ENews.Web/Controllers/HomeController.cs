using System.Collections.Generic;
using System.Diagnostics;
using ENews.Services.Data;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace ENews.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IArticlesService articleService;
        private readonly IUsersService usersService;

        public HomeController(
            IArticlesService articleService,
            IUsersService usersService)
        {
            this.articleService = articleService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                LatestArticle = this.articleService.GetLastByCreatedOn<ArticlePreviewViewModel>(),
                LatestTwoArticles = this.articleService.GetLatesByCreatedOn<ArticlePreviewViewModel>(2, 1),
                LatestPopularNews = this.articleService.GetLatesMostViewed<ArticlePreviewViewModel>(4),
                LatestWorldNews = this.articleService.GetLatesInternationalArticles<ArticlePreviewViewModel>(9),
                LatestVideos = this.articleService.GetLatesWithVideos<ArticlesVideoPreview>(3),
            };
            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult About()
        {
            var model = new AboutViewModel()
            {
                TopEmployees = this.usersService.GetTopMembers<EmployeeViewModel>(),
                ArticlesCount = this.articleService.GetCount(),
                MembersCount = this.usersService.GetCountOfMembers(),
                UsersCount = this.usersService.GetCountOfUsers(),
                ArticleReads = this.articleService.GetSumAllSeens(),
            };

            return this.View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
