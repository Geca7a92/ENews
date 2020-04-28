using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Comments
{
    public class CommentPreviewViewModel : IMapFrom<Comment>
    {
        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureImageUrl { get; set; }

        public int ArticleId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
