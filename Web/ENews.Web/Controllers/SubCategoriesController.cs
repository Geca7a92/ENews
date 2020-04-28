namespace ENews.Web.Controllers
{
    using ENews.Common;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoriesService subCategoriesService;
        private readonly IArticlesService articleService;
        private readonly IPagingService pagingService;

        public SubCategoriesController(
            ISubCategoriesService subCategoriesService,
            IArticlesService articleService,
            IPagingService pagingService)
        {
            this.subCategoriesService = subCategoriesService;
            this.articleService = articleService;
            this.pagingService = pagingService;
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

            var skip = this.pagingService.CountSkip(page);

            viewModel.SubCategoryArticles = this.articleService.GetAllBySubCategoryId<ArticlePreviewViewModel>(viewModel.Id, GlobalConstants.ArticlePerPage, skip);

            var articlesCount = this.articleService.GetCountBySubCategoryId(viewModel.Id);

            viewModel.PagesCount = this.pagingService.PagesCount(articlesCount);

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
