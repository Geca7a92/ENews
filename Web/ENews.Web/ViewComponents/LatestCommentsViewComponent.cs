using ENews.Services.Data;
using ENews.Web.ViewModels.Comments;
using ENews.Web.ViewModels.Shared.Components.LatestComments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.ViewComponents
{
    public class LatestCommentsViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public LatestCommentsViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new LatestCommentsViewModel();

            model.Comments = this.commentsService.GetLatesByCreatedOn<CommentPreviewViewModel>(4);

            return this.View(model);
        }
    }
}
