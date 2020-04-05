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
        private readonly IArticleService articleService;

        public CategoriesController(ICategoriesService categoriesService, IArticleService articleService)
        {
            this.categoriesService = categoriesService;
            this.articleService = articleService;
        }

        public IActionResult Index(string name)
        {
            var viewModel = this.categoriesService.GetCategoryByName<CategoryViewModel>(name);
            return this.View(viewModel);
        }

        public IActionResult BySubCategory(string name)
        {
            var viewModel = new SubCategoryArticlesViewModel()
            {
                Articles = this.articleService.GetArticlesBySubCategoryName<ArticleViewModel>(name),
                Title = name,
            };
            return this.View(viewModel);
        }
    }
}
