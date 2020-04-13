﻿using ENews.Common;
using ENews.Data.Models;
using ENews.Services.Data;
using ENews.Web.ViewModels.MembersArea.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ENews.Web.Areas.MembersArea.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.ReporterRoleName)]
    [Area("MembersArea")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleController(
            IArticleService articleService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAllCategories<CategoriesDropDownViewModel>();
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
                input.CategoriesDropDown = this.categoriesService.GetAllCategories<CategoriesDropDownViewModel>();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var articleId = await this.articleService.CreateAsync(input, user.Id);

            return this.RedirectToAction("Index", "Articles", new { area = string.Empty, id = articleId });
        }

        public IActionResult GetSubcategories(int id)
        {
            var subCategories = this.categoriesService
                .GetSubCategoriesOfCategoryId<SubCategoriesDropDownViewModel>(id);
            return this.Json(subCategories);
        }
    }
}
