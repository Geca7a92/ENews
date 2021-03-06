﻿namespace ENews.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.MembersArea.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articleService;
        private readonly ICategoriesService categoriesService;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly IPagingService pagingService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesController(
            IArticlesService articleService,
            ICategoriesService categoriesService,
            ISubCategoriesService subCategoriesService,
            IPagingService pagingService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.categoriesService = categoriesService;
            this.subCategoriesService = subCategoriesService;
            this.pagingService = pagingService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllActive(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var user = await this.userManager.GetUserAsync(this.User);

            var result = this.articleService.GetAllActive<IndexArticleViewModel>(GlobalConstants.AdministrationItemsPerPage, skip);

            var articlesCount = this.articleService.GetCountByAuthorId(user.Id);

            var model = new IndexArticlesViewModel()
            {
                Articles = result,
                PagesCount = this.pagingService.PagesCount(articlesCount, GlobalConstants.AdministrationItemsPerPage),
            };

            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

            return this.View(model);
        }

        public async Task<IActionResult> AllDeleted(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var user = await this.userManager.GetUserAsync(this.User);

            var result = this.articleService.GetAllDeleted<IndexArticleViewModel>(GlobalConstants.AdministrationItemsPerPage, skip);

            var articlesCount = this.articleService.GetCountByAuthorIdDeleted(user.Id);

            var model = new IndexArticlesViewModel()
            {
                Articles = result,
                PagesCount = this.pagingService.PagesCount(articlesCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

            return this.View(model);
        }

        public async Task<IActionResult> HardDelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var article = await this.articleService.GetArticleByIdWithDeleted<IndexArticleViewModel>((int)id);
            if (article == null)
            {
                return this.NotFound();
            }

            return this.View(article);
        }

        [HttpPost]
        [ActionName("HardDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.articleService.HardDeleteById(id);

            return this.RedirectToAction(nameof(this.AllActive));
        }
    }
}
