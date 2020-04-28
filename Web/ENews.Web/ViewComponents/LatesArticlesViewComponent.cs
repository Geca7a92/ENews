namespace ENews.Web.ViewComponents
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Shared.Components.LatestArticles;
    using Microsoft.AspNetCore.Mvc;

    public class LatesArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public LatesArticlesViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke(int count, int skip = 0)
        {
            var model = new LatestArticlesViewModel
            {
                Articles = this.articleService.GetLatesByCreatedOn<ArticlePreviewViewModel>(count, skip),
            };
            return this.View(model);
        }
    }
}
