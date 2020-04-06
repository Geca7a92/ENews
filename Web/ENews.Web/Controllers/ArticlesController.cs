using ENews.Services.Data;
using ENews.Web.ViewModels.Articles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;

        public ArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Index(int id)
        {
            var model = this.articleService.PreviewArticleById<ArticleViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult CreateComment()
        {
            return this.View();
        }
    }
}
