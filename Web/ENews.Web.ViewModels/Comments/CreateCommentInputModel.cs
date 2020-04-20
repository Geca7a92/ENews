using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int ArticleId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
