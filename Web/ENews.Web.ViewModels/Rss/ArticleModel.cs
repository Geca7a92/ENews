namespace ENews.Web.ViewModels.Rss
{
    using System;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using Ganss.XSS;

    public class ArticleRssModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string PictureImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
    }
}
