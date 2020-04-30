namespace ENews.Web.ViewComponents
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Shared.Components.LatestHeadlines;
    using Microsoft.AspNetCore.Mvc;

    public class LatestInternationalHeadlinesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public LatestInternationalHeadlinesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new LatestHeadlinesViewModel
            {
                Articles = this.articleService.GetLatesInternationalArticles<LatestHeadlineViewModel>(3),
            };

            return this.View(model);
        }
    }
}
