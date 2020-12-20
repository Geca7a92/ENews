namespace ENews.Services.Data.Tests.Models
{
    using System;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Comments;
    using Ganss.XSS;

    public class ArticleDumyModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public int CategoryId { get; set; }

        public string SubCategoryTitle { get; set; }

        public string PictureImageUrl { get; set; }

        public int SeenCount { get; set; }

        public int CommentsCount { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string AuthorBiography { get; set; }

        public string AuthorUserName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorProfilePictureImageUrl { get; set; }

        public string AuthorLastName { get; set; }

        public int AuthorArticlesCount { get; set; }

        public DateTime AuthorCreatedOn { get; set; }

        public string AuthorFullName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public DateTime CreatedOn { get; set; }

        public int? GalleryId { get; set; }

        public CreateCommentInputModel CommentModel { get; set; }
    }
}
