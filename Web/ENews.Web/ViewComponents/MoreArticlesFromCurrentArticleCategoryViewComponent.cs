namespace ENews.Web.ViewComponents
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Shared.Components.MoreArticlesFromCurrentArticleCategory;
    using Microsoft.AspNetCore.Mvc;

    public class MoreArticlesFromCurrentArticleCategoryViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public MoreArticlesFromCurrentArticleCategoryViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke(int categoryId, int take, int articleId, int skip = 0)
        {
            var model = new MoreArticlesFromCurrentArticleCategoryViewModel
            {
                Articles = this.articleService.GetAllByCategoryId<ArticlePreviewViewModel>(categoryId, take, skip, articleId),
            };

            return this.View(model);
        }
    }
}
