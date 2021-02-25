namespace ENews.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Articles;
    using ENews.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel()
            {
                LatestArticle = await this.articleService.GetLastByCreatedOn<ArticlePreviewViewModel>(),
                LatestTwoArticles = await this.articleService.GetLatesByCreatedOnAsync<ArticleBaseViewModel>(2, 1),
                LatestPopularNews = await this.articleService.GetLatesMostViewedAsync<ArticleBaseViewModel>(4),
                LatestWorldNews = await this.articleService.GetLatesInternationalArticlesAsync<ArticleBaseViewModel>(9),
                LatestVideos = await this.articleService.GetLatesWithVideosAsync<ArticlesVideoPreview>(3),
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
