using ENews.Common;
using ENews.Services.Data;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.SubCategories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoriesService categoriesService;

        public CategoriesController(IArticleService articleService, ICategoriesService categoriesService)
        {
            this.articleService = articleService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index(string name, int page = 1)
        {
            var viewModel = this.categoriesService.GetCategoryByName<MainCategoryArticlesViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            if (page < 1)
            {
                page = 1;
            }

            var skip = (page - 1) * GlobalConstants.ArticlePerPage;

            viewModel.CategoryArticles = this.articleService.GetAllByCategoryId<ArticlePreviewViewModel>(viewModel.Id, GlobalConstants.ArticlePerPage, skip);

            var count = this.articleService.GetCountByCategoryId(viewModel.Id);

            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ArticlePerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
