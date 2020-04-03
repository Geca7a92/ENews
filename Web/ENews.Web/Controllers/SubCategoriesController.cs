using ENews.Services.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public SubCategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index(string name)
        {
            return this.View(name);
        }
    }
}
