﻿namespace ENews.Web.Controllers
{
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models.Enums;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Categories;
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

        public async Task<IActionResult> Index(string data, int page = 1)
        {
            var viewModel = await this.categoriesService.GetCategoryByName<MainCategoryArticlesViewModel>(data);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Route = GlobalConstants.CategoryRoute;
            viewModel.Data = data;

            var skip = this.pagingService.CountSkip(page, GlobalConstants.ArticlePerPage);

            viewModel.CategoryArticles = this.articlesService.GetAllByCategoryId<ArticlePreviewViewModel>(viewModel.Id, GlobalConstants.ArticlePerPage, skip);

            var articlesCount = this.articlesService.GetCountByCategoryId(viewModel.Id);

            viewModel.PagesCount = this.pagingService.PagesCount(articlesCount, GlobalConstants.ArticlePerPage);

            viewModel.CurrentPage = this.pagingService.SetPage(page, viewModel.PagesCount);

            return this.View(viewModel);
        }

        public IActionResult Local(int page = 1)
        {
            var viewModel = new LocalArticlesViewModel();

            viewModel.Route = GlobalConstants.LocalRoute;

            var skip = this.pagingService.CountSkip(page, GlobalConstants.ArticlePerPage);

            viewModel.CategoryArticles = this.articlesService.GetLatesLocalArticles<ArticlePreviewViewModel>(GlobalConstants.ArticlePerPage, skip);

            viewModel.PagesCount = this.pagingService.GetPagesCountByRegion(null);

            viewModel.CurrentPage = this.pagingService.SetPage(page, viewModel.PagesCount);

            return this.View(viewModel);
        }

        public IActionResult LocalByRegion(Region data, int page = 1)
        {
            if (data == 0)
            {
                return this.NotFound();
            }

            var viewModel = new RegionArticlesViewModel() { Data = data.ToString() };

            viewModel.Route = GlobalConstants.LocalByRegionRoute;

            var skip = this.pagingService.CountSkip(page, GlobalConstants.ArticlePerPage);

            viewModel.CategoryArticles = this.articlesService.GetLatestByRegion<ArticlePreviewViewModel>(data, GlobalConstants.ArticlePerPage, skip);

            viewModel.PagesCount = this.pagingService.GetPagesCountByRegion(data);

            viewModel.CurrentPage = this.pagingService.SetPage(page, viewModel.PagesCount);

            return this.View(viewModel);
        }
    }
}
