using ENews.Data.Models;
using ENews.Services.Mapping;
using ENews.Web.ViewModels.Images;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ENews.Web.ViewModels.Galleries
{
    public class GalleryPreviewViewModel : IMapFrom<Gallery>
    {
        public int Id { get; set; }

        public IEnumerable<ImagePreviewViewModel> Images { get; set; }

        public int ArticleId { get; set; }

        public string ArticleTitle { get; set; }

        public string ArticleAuthorUserName { get; set; }

        public string ArticleContent { get; set; }

        public string ContetPreview
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.ArticleContent))
                {
                    var content = WebUtility.HtmlDecode(Regex.Replace(this.ArticleContent, @"<[^>]+>", string.Empty));

                    return content.Length > 100 ? content.Substring(0, 100) + "..." : content;
                }

                return this.ArticleTitle;
            }
        }
    }
}
