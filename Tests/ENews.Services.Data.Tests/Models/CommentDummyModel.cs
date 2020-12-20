namespace ENews.Services.Data.Tests.Models
{
    using System;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CommentDummyModel : IMapFrom<Comment>
    {
        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public int ArticleId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
