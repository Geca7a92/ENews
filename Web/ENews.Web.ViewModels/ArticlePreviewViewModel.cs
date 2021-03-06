﻿namespace ENews.Web.ViewModels
{
    using System.Net;
    using System.Text.RegularExpressions;

    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Web.ViewModels.Articles;

    public class ArticlePreviewViewModel : ArticleBaseViewModel
    {
        public string AuthorUserName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorFullName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public int? GalleryId { get; set; }

        public Gallery Gallery { get; set; }

        public string Content { get; set; }

        public Region? Region { get; set; }

        public string ContetPreview
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));

                return content.Length > 100 ? content.Substring(0, 100) + "..." : content;
            }
        }

        public string PreviewTitle => this.Title + (this.GalleryId != null ? "(Gallery)" : string.Empty);
    }
}
