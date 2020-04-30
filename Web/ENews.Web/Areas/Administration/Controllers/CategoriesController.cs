namespace ENews.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Administration.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IPagingService pagingService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IPagingService pagingService)
        {
            this.categoriesService = categoriesService;
            this.pagingService = pagingService;
        }

        public IActionResult Active(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.categoriesService.GetAllCategories<IndexCategoryViewModel>(GlobalConstants.AdministrationItemsPerPage, skip);

            var categoriesCount = this.categoriesService.GetCountOfActiveCategories();

            var model = new IndexCategoriesViewModel()
            {
                Categories = result,
                PagesCount = this.pagingService.PagesCount(categoriesCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

            return this.View(model);
        }

        public IActionResult Deleted(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.categoriesService.GetAllDeletedCategories<IndexCategoryViewModel>(GlobalConstants.AdministrationItemsPerPage, skip);

            var categoriesCount = this.categoriesService.GetCountOfDeletedCategories();

            var model = new IndexCategoriesViewModel()
            {
                Categories = result,
                PagesCount = this.pagingService.PagesCount(categoriesCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.categoriesService.GetCategoryModelById((int)id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (await this.categoriesService.CategoryExistsByName(category.Title))
            {
                this.ModelState.AddModelError(category.Title, $"Title - {category.Title} already exists");
                return this.View(category);
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.categoriesService.UpdateCategory(category);
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

                return this.RedirectToAction(nameof(this.Active));
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
            return this.RedirectToAction(nameof(this.Active));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.categoriesService.DeleteById((int)id);

            return this.RedirectToAction(nameof(this.Active));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.categoriesService.UndeleteById((int)id);

            return this.RedirectToAction(nameof(this.Deleted));
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

            return this.RedirectToAction(nameof(this.Active));
        }
    }
}
