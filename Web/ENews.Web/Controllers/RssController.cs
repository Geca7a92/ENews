using ENews.Services.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Controllers
{
    public class RssController : Controller
    {
        private readonly IArticlesService articlesService;

        public RssController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public ActionResult ENews()
        {
            var articles = this.articlesService.GetAll<>(10);
        }
    }
}
