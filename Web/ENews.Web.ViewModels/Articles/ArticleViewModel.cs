﻿namespace ENews.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using Ganss.XSS;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public string SubCategoryTitle { get; set; }

        public string PictureImageUrl { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public int AuthorArticlesCount { get; set; }

        public DateTime AuthorCreatedOn { get; set; }

        public string AuthorFullName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public DateTime CreatedOn { get; set; }

        public int? GalleryId { get; set; }

        public ICollection<ArticleCommentsViewModel> Comments { get; set; }
    }
}
