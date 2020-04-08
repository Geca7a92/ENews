using ENews.Common;
using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Web.ViewModels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Areas.MembersArea.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.ReporterRoleName)]
    [Area("MembersArea")]
    public class ArticleController : Controller
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleController(
            IDeletableEntityRepository<Article> articlesRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.articlesRepository = articlesRepository;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            model.AuthorName = user.FirstName + " " + user.LastName;
            var article = new Article
            {
                AuthorId = user.Id,
                Title = model.Title,
                Content = model.Content,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId,
                PictureId = 2,
            };

            await this.articlesRepository.AddAsync(article);
            await this.articlesRepository.SaveChangesAsync();

            return this.RedirectToAction("Index", "Articles", new { area = string.Empty, id = article.Id });
        }
    }
}
