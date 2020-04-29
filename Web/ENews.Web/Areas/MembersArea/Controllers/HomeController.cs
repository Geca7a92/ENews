namespace ENews.Web.Areas.MembersArea.Controllers
{
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.MembersArea.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.ReporterRoleName)]
    [Area("MembersArea")]
    public class HomeController : Controller
    {
        private readonly IArticlesService articleService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IArticlesService articleService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Active()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = this.articleService.GetAllByAtuthorId<IndexArticleViewModel>(user.Id);
            var model = new IndexArticlesViewModel()
            {
                Articles = result,
            };
            return this.View(model);
        }

        public async Task<IActionResult> Deleted()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = this.articleService.GetAllByAtuthorIdDeleted<IndexArticleViewModel>(user.Id);
            var model = new IndexArticlesViewModel()
            {
                Articles = result,
            };
            return this.View(model);
        }
    }
}
