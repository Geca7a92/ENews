namespace ENews.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Administration.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly ICategoriesService categoriesService;

        public CategoriesController(
            ApplicationDbContext context,
            IDeletableEntityRepository<Category> categoryRepository,
            ICategoriesService categoriesService)
        {
            this.context = context;
            this.categoryRepository = categoryRepository;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var result = this.categoriesService.GetAllWithDeletedCategories<IndexCategoryViewModel>();
            var model = new IndexCategoriesViewModel()
            {
                Categories = result,
            };
            return this.View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.categoriesService.GetCategoryById<IndexCategoryViewModel>((int)id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // ToDo Service
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.categoryRepository.GetByIdWithDeletedAsync(id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // ToDo Service
        // Fix Unique Title when Edditing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Title,Description,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(category);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.categoriesService.CategoryExistsById(category.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(category);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            if (await this.categoriesService.CategoryExistsByName(inputModel.Title))
            {
                this.ModelState.AddModelError(inputModel.Title, $"Title - {inputModel.Title} already exists");
                return this.View(inputModel);
            }

            await this.categoriesService.CreateCategoryAsync(inputModel);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.categoriesService.DeleteById((int)id);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.categoriesService.UndeleteById((int)id);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> HardDelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.categoriesService.GetCategoryById<IndexCategoryViewModel>((int)id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ActionName("HardDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.categoriesService.HardDeleteById(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
