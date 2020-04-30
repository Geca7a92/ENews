namespace ENews.Web.Controllers
{
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Index(int id)
        {
            var model = await this.articleService.GetArticleById<ArticleViewModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            model.SeenCount = await this.articleService.AddToSeenCount(model.Id);
            return this.View(model);
        }
    }
}
