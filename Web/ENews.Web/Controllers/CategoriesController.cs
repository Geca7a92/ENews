using ENews.Services.Data;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index(string name)
        {
            var viewModel = this.categoriesService.GetCategoryByName<MainCategoryArticlesViewModel>(name);
            return this.View(viewModel);
        }

        public IActionResult BySubCategory(string name)
        {
            var viewModel = this.categoriesService.GetSubCategoryByName<SubCategoryArticlesViewModel>(name);
            return this.View(viewModel);
        }
    }
}
