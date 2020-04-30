namespace ENews.Web.ViewComponents
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Shared.Components.LatestHeadlines;
    using Microsoft.AspNetCore.Mvc;

    public class LatestHeadlinesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public LatestHeadlinesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new LatestHeadlinesViewModel();

            model.Articles = this.articleService.GetLatesByCreatedOn<LatestHeadlineViewModel>(3);

            return this.View(model);
        }
    }
}
