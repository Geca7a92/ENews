namespace ENews.Web.Controllers
{
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Articles;
    using ENews.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IArticlesService articlesService;
        private readonly IPagingService pagingService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IUsersService usersService,
            IArticlesService articlesService,
            IPagingService pagingService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.articlesService = articlesService;
            this.pagingService = pagingService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string username)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var model = await this.usersService.GetUserByUsername<UserProfileViewModel>(username);
            if (user != null)
            {
                model.MyProfile = user.Id == model.Id;
            }

            var result = this.articlesService.GetAllByAtuthorId<ArticleBaseViewModel>(model.Id, 4, 0);
            model.MyArticles = result;
            return this.View(model);
        }

        public async Task<IActionResult> Articles(string data, int page = 1)
        {
            var viewModel = await this.usersService.GetUserByUsername<UserArticlesViewModel>(data);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Route = GlobalConstants.UsernameRoute;
            viewModel.Data = data;

            var skip = this.pagingService.CountSkip(page, GlobalConstants.ArticlePerPage);

            viewModel.UserArticles = this.articlesService.GetAllByAtuthorId<ArticlePreviewViewModel>(viewModel.Id, GlobalConstants.ArticlePerPage, skip);

            var articlesCount = this.articlesService.GetCountByAuthorId(viewModel.Id);

            viewModel.PagesCount = this.pagingService.PagesCount(articlesCount, GlobalConstants.ArticlePerPage);

            viewModel.CurrentPage = this.pagingService.SetPage(page, viewModel.PagesCount);

            return this.View(viewModel);
        }
    }
}
