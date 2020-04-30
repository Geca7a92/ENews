namespace ENews.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data;
    using ENews.Data.Models;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Administration.Categories;
    using ENews.Web.ViewModels.Administration.SubCategories;
    using ENews.Web.ViewModels.MembersArea.Articles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly IPagingService pagingService;
        private readonly ICategoriesService categoriesService;

        public SubCategoriesController(
            ApplicationDbContext context,
            ISubCategoriesService subCategoriesService,
            IPagingService pagingService,
            ICategoriesService categoriesService)
        {
            this.context = context;
            this.subCategoriesService = subCategoriesService;
            this.pagingService = pagingService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Active(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.subCategoriesService.GetAllSubCategories<IndexSubCategoryViewModel>(GlobalConstants.AdministrationItemsPerPage, skip);

            var subCategoriesCount = this.subCategoriesService.GetCountOfActiveSubCategories();

            var model = new IndexSubCategoriesViewModel()
            {
                SubCategories = result,
                PagesCount = this.pagingService.PagesCount(subCategoriesCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

            return this.View(model);
        }

        public IActionResult Deleted(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.subCategoriesService.GetAllDeletedSubCategories<IndexSubCategoryViewModel>(GlobalConstants.AdministrationItemsPerPage, skip);

            var subCategoriesCount = this.subCategoriesService.GetCountOfDeletedSubCategories();

            var model = new IndexSubCategoriesViewModel()
            {
                SubCategories = result,
                PagesCount = this.pagingService.SetPage(subCategoriesCount, GlobalConstants.AdministrationItemsPerPage),
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
            return this.RedirectToAction(nameof(this.Active));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var subCategory = await this.subCategoriesService.GetSubCategoryModelById((int)id);

            if (subCategory == null)
            {
                return this.NotFound();
            }

            var categories = this.categoriesService.GetAllCategories<CategoriesDropDownViewModel>();
            this.ViewData["CategoryId"] = new SelectList(categories, "Id", "Title", subCategory.CategoryId);
            return this.View(subCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] SubCategory subCategory)
        {
            if (id != subCategory.Id)
            {
                return this.NotFound();
            }

            var categories = this.categoriesService.GetAllCategories<DropDownCategories>();
            if (await this.subCategoriesService.SubCategoryExistsByName(subCategory.Title, subCategory.CategoryId))
            {
                this.ViewData["CategoryId"] = new SelectList(categories, "Id", "Title", subCategory.CategoryId);
                this.ModelState.AddModelError(subCategory.Title, $"Title - {subCategory.Title} already exists");
                return this.View(subCategory);
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.subCategoriesService.Update(subCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.subCategoriesService.SubCategoryExistsById(subCategory.Id))
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

            this.ViewData["CategoryId"] = new SelectList(categories, "Id", "Title", subCategory.CategoryId);
            return this.View(subCategory);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.subCategoriesService.DeleteById((int)id);

            return this.RedirectToAction(nameof(this.Active));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.subCategoriesService.UndeleteById((int)id);

            return this.RedirectToAction(nameof(this.Deleted));
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

            return this.RedirectToAction(nameof(this.Active));
        }
    }
}
