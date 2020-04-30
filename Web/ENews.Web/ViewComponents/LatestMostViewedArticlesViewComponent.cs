namespace ENews.Web.ViewComponents
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Shared.Components.LatestMostViewdArticles;
    using Microsoft.AspNetCore.Mvc;

    public class LatestMostViewedArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public LatestMostViewedArticlesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke(int count, int skip = 0)
        {
            var model = new LatestMostViewedArticlesViewModel
            {
                Articles = this.articleService.GetLatesMostViewed<ArticlePreviewViewModel>(count, skip),
            };
            return this.View(model);
        }
    }
}
