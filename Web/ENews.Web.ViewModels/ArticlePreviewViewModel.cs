using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace ENews.Web.ViewModels
{

    public class ArticlePreviewViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public string SubCategoryTitle { get; set; }

        public string PictureImageUrl { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorFullName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public int? GalleryId { get; set; }

        public Gallery Gallery { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

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
