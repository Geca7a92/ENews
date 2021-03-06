﻿namespace ENews.Web.ViewComponents
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Shared.Components.MostCommentedNews;
    using Microsoft.AspNetCore.Mvc;

    public class MostCommentedNewsViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;

        public MostCommentedNewsViewComponent(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IViewComponentResult Invoke(int count, int skip = 0)
        {
            var model = new MostCommentedNewsViewModel
            {
                Articles = this.articleService.GetLatesMostCommented<ArticlePreviewViewModel>(count, skip),
            };

            return this.View(model);
        }
    }
}
