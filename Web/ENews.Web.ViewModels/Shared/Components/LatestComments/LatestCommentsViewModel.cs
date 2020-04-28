namespace ENews.Web.ViewModels.Shared.Components.LatestComments
{
    using System.Collections.Generic;

    using ENews.Web.ViewModels.Comments;

    public class LatestCommentsViewModel
    {
        public IEnumerable<CommentPreviewViewModel> Comments { get; set; }
    }
}
