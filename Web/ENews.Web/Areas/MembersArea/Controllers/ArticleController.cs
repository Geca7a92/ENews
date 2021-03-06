﻿namespace ENews.Web.Areas.MembersArea.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.MembersArea.Articles;
    using ENews.Web.ViewModels.MembersArea.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.ReporterRoleName)]
    [Area("MembersArea")]
    public class ArticleController : Controller
    {
        private readonly IArticlesService articleService;
        private readonly ICategoriesService categoriesService;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly IPagingService pagingService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleController(
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

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAllCategoriesWithAnySubCategories<CategoriesDropDownViewModel>();
            var viewModel = new ArticleCreateInputModel()
            {
                CategoriesDropDown = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesDropDown = this.categoriesService.GetAllCategoriesWithAnySubCategories<CategoriesDropDownViewModel>();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var articleId = await this.articleService.CreateAsync(input, user.Id);

            return this.RedirectToAction("Index", "Articles", new { area = string.Empty, id = articleId });
        }

        public async Task<IActionResult> Active(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var user = await this.userManager.GetUserAsync(this.User);

            var result = this.articleService.GetAllByAtuthorId<IndexArticleViewModel>(user.Id, GlobalConstants.AdministrationItemsPerPage, skip);

            var articlesCount = this.articleService.GetCountByAuthorId(user.Id);

            var model = new IndexArticlesViewModel()
            {
                Articles = result,
                PagesCount = this.pagingService.PagesCount(articlesCount, GlobalConstants.AdministrationItemsPerPage),
            };

            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

            return this.View(model);
        }

        public async Task<IActionResult> Deleted(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var user = await this.userManager.GetUserAsync(this.User);

            var result = this.articleService.GetAllByAtuthorIdDeleted<IndexArticleViewModel>(user.Id, GlobalConstants.AdministrationItemsPerPage, skip);

            var articlesCount = this.articleService.GetCountByAuthorIdDeleted(user.Id);

            var model = new IndexArticlesViewModel()
            {
                Articles = result,
                PagesCount = this.pagingService.PagesCount(articlesCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

            return this.View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.articleService.DeleteById((int)id);

            return this.RedirectToAction(nameof(this.Active));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var article = await this.articleService.GetArticleByIdWithDeleted<DetailsArticleViewModel>((int)id);
            if (article == null)
            {
                return this.NotFound();
            }

            return this.View(article);
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.articleService.UndeleteById((int)id);

            return this.RedirectToAction(nameof(this.Deleted));
        }

        public IActionResult GetSubcategories(int id)
        {
            var subCategories = this.subCategoriesService
                .GetSubCategoriesOfCategoryId<SubCategoriesDropDownViewModel>(id);
            return this.Json(subCategories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var article = await this.articleService.GetArticleById<ArticleCreateInputModel>((int)id);

            var currentUser = await this.userManager.GetUserAsync(this.User);

            var isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);
            var isOwner = await this.articleService.CheckArticleOwnership(currentUser.Id, (int)id);

            if (!isOwner && !isAdmin)
            {
                return this.Redirect("/MembersArea/Article/Active");
            }

            article.CategoriesDropDown = this.categoriesService.GetAllCategoriesWithAnySubCategories<CategoriesDropDownViewModel>();

            if (article == null)
            {
                return this.NotFound();
            }

            return this.View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleCreateInputModel input, int id)
        {
            this.ModelState.Remove("MainImage");
            if (!this.ModelState.IsValid)
            {
                input.CategoriesDropDown = this.categoriesService.GetAllCategoriesWithAnySubCategories<CategoriesDropDownViewModel>();
                return this.View(input);
            }

            await this.articleService.Update(input, id);

            return this.RedirectToAction(nameof(this.Active));
        }
    }
}
