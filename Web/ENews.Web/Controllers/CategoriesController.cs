namespace ENews.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models.Enums;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Categories;
    using ENews.Web.ViewModels.SubCategories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly ICategoriesService categoriesService;
        private readonly IPagingService pagingService;

        public CategoriesController(
            IArticlesService articlesService, 
            ICategoriesService categoriesService,
            IPagingService pagingService)
        {
            this.articlesService = articlesService;
            this.categoriesService = categoriesService;
            this.pagingService = pagingService;
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

            var skip = this.pagingService.CountSkip(page);

            viewModel.CategoryArticles = this.articlesService.GetAllByCategoryId<ArticlePreviewViewModel>(viewModel.Id, GlobalConstants.ArticlePerPage, skip);

            var articlesCount = this.articlesService.GetCountByCategoryId(viewModel.Id);

            viewModel.PagesCount = this.pagingService.PagesCount(articlesCount);

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult Local(int page = 1)
        {
            var viewModel = new LocalArticlesViewModel();

            if (page < 1)
            {
                page = 1;
            }

            var skip = this.pagingService.CountSkip(page);

            viewModel.CategoryArticles = this.articlesService.GetLatesLocalArticles<ArticlePreviewViewModel>(GlobalConstants.ArticlePerPage, skip);

            viewModel.PagesCount = this.pagingService.GetPagesCountByRegion(null);

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult LocalByRegion(Region region, int page = 1)
        {
            if (region == 0)
            {
                return this.NotFound();
            }

            var viewModel = new RegionArticlesViewModel() { Region = region };

            if (page < 1)
            {
                page = 1;
            }

            var skip = this.pagingService.CountSkip(page);

            viewModel.CategoryArticles = this.articlesService.GetLatestByRegion<ArticlePreviewViewModel>(region, GlobalConstants.ArticlePerPage, skip);

            viewModel.PagesCount = this.pagingService.GetPagesCountByRegion(region);

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
