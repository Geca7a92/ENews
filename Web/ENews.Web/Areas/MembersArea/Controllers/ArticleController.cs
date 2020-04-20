using ENews.Common;
using ENews.Data.Models;
using ENews.Services.Data;
using ENews.Web.ViewModels.MembersArea.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace ENews.Web.Areas.MembersArea.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.ReporterRoleName)]
    [Area("MembersArea")]
    public class ArticleController : Controller
    {
        private readonly IArticlesService articleService;
        private readonly ICategoriesService categoriesService;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleController(
            IArticlesService articleService,
            ICategoriesService categoriesService,
            ISubCategoriesService subCategoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.categoriesService = categoriesService;
            this.subCategoriesService = subCategoriesService;
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

        public IActionResult GetSubcategories(int id)
        {
            var subCategories = this.subCategoriesService
                .GetSubCategoriesOfCategoryId<SubCategoriesDropDownViewModel>(id);
            return this.Json(subCategories);
        }
    }
}
