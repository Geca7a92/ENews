namespace ENews.Web.Controllers
{
    using ENews.Common;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index(string data, int page = 1)
        {
            var viewModel = this.subCategoriesService.GetSubCategoryByName<SubCategoryArticlesViewModel>(data);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            var skip = this.pagingService.CountSkip(page, GlobalConstants.ArticlePerPage);
            viewModel.Route = GlobalConstants.SubCategoryRoute;
            viewModel.Data = data;
            viewModel.SubData = viewModel.CategoryTitle;

            viewModel.SubCategoryArticles = this.articleService.GetAllBySubCategoryId<ArticlePreviewViewModel>(viewModel.Id, GlobalConstants.ArticlePerPage, skip);

            var articlesCount = this.articleService.GetCountBySubCategoryId(viewModel.Id);

            viewModel.PagesCount = this.pagingService.PagesCount(articlesCount, GlobalConstants.ArticlePerPage);

            viewModel.CurrentPage = this.pagingService.SetPage(page, viewModel.PagesCount);

            return this.View(viewModel);
        }
    }
}
