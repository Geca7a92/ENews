namespace ENews.Web.Controllers
{
    using ENews.Common;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.SubCategories;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoriesService subCategoriesService;
        private readonly IArticlesService articleService;

        public SubCategoriesController(
            ISubCategoriesService subCategoriesService,
            IArticlesService articleService)
        {
            this.subCategoriesService = subCategoriesService;
            this.articleService = articleService;
        }

        public IActionResult Index(string subCategoryName, int page = 1)
        {
            var viewModel = this.subCategoriesService.GetSubCategoryByName<SubCategoryArticlesViewModel>(subCategoryName);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            if (page < 1)
            {
                page = 1;
            }

            var skip = (page - 1) * GlobalConstants.ArticlePerPage;

            viewModel.SubCategoryArticles = this.articleService.GetAllBySubCategoryId<ArticlePreviewViewModel>(viewModel.Id, GlobalConstants.ArticlePerPage, skip);

            var count = this.articleService.GetCountBySubCategoryId(viewModel.Id);

            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ArticlePerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
