﻿using ENews.Common;
using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Services.Data;
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
        private readonly IArticleService articleService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleController(
            IArticleService articleService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var articleId = await this.articleService.CreateAsync(input, user.Id);

            return this.RedirectToAction("Index", "Articles", new { area = string.Empty, id = articleId });
        }
    }
}
