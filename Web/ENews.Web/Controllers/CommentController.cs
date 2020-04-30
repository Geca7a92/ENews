namespace ENews.Web.Controllers
{
    using System.Threading.Tasks;

    using ENews.Data.Models;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IArticlesService articlesService;

        public CommentController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager,
            IArticlesService articlesService)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.articlesService = articlesService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentInputModel model)
        {
            if (!this.articlesService.ArticleExist(model.ArticleId))
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            model.UserId = user.Id;
            await this.commentsService.CreateAsync(model);
            return this.RedirectToAction("Index", "Articles", new { id = model.ArticleId });
        }
    }
}
