using ENews.Common;
using ENews.Data.Models;
using ENews.Services.Data;
using ENews.Web.ViewModels.MembersArea.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Areas.MembersArea.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.ReporterRoleName)]
    [Area("MembersArea")]
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IArticleService articleService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = this.articleService.GetAllByAtuthorId<IndexArticleViewModel>(user.Id);
            var model = new IndexArticlesViewModel()
            {
                Articles = result,
            };
            return this.View(model);
        }
    }
}
