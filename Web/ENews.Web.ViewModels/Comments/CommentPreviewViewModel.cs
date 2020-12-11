namespace ENews.Web.ViewModels.Comments
{
    using System;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CommentPreviewViewModel : IMapFrom<Comment>
    {
        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureImageUrl { get; set; }

        public string ArticleTitle { get; set; }

        public int ArticleId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
