using ENews.Common;
using ENews.Data;
using ENews.Data.Models;
using ENews.Services.Data;
using ENews.Web.ViewModels.Administration.SubCategories;
using ENews.Web.ViewModels.MembersArea.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly ICategoriesService categoriesService;

        public SubCategoriesController(
            ApplicationDbContext context,
            ISubCategoriesService subCategoriesService,
            ICategoriesService categoriesService)
        {
            this.context = context;
            this.subCategoriesService = subCategoriesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var result = this.subCategoriesService.GetAllWithDeletedSubCategories<IndexSubCategoryViewModel>();
            var model = new IndexSubCategoriesViewModel()
            {
                SubCategories = result,
            };
            return this.View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.subCategoriesService.GetSubCategoryById<IndexSubCategoryViewModel>((int)id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAllCategories<CategoriesDropDownViewModel>();
            var viewModel = new SubCategoryCreateInputModel()
            {
                CategoriesDropDown = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryCreateInputModel inputModel)
        {
            inputModel.CategoriesDropDown = this.categoriesService.GetAllCategories<CategoriesDropDownViewModel>();
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            if (await this.subCategoriesService.SubCategoryExistsByName(inputModel.Title))
            {
                this.ModelState.AddModelError(inputModel.Title, $"Title - {inputModel.Title} already exists");
                return this.View(inputModel);
            }

            if (!await this.categoriesService.CategoryExistsById(inputModel.CategoryId))
            {
                this.ModelState.AddModelError("Category", "Select appropriate category!");
                return this.View(inputModel);
            }

            await this.subCategoriesService.CreateSubCategoryAsync(inputModel);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.subCategoriesService.GetSubCategoryById<SubCategoryCreateInputModel>((int)id);

            category.CategoriesDropDown = this.categoriesService.GetAllCategories<CategoriesDropDownViewModel>();

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.subCategoriesService.DeleteById((int)id);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.subCategoriesService.UndeleteById((int)id);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> HardDelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var subCategory = await this.subCategoriesService.GetSubCategoryById<IndexSubCategoryViewModel>((int)id);
            if (subCategory == null)
            {
                return this.NotFound();
            }

            return this.View(subCategory);
        }

        [HttpPost]
        [ActionName("HardDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.subCategoriesService.HardDeleteById(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
