using ENews.Data.Models;
using ENews.Services.Data;
using ENews.Web.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        //FIX WHEN COMMENT AND NOT LOGGED IN ERROR
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            model.UserId = user.Id;
            await this.commentsService.Create(model);
            return this.RedirectToAction("Index", "Articles", new { id = model.ArticleId });
        }
    }
}
