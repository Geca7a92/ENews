namespace ENews.Web.Controllers
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Articles;
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : Controller
    {
        private readonly IArticlesService articleService;

        public ArticlesController(IArticlesService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Index(int id)
        {
            var model = this.articleService.GetArticleById<ArticleViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }
    }
}
