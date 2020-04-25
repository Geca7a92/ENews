using ENews.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Shared.Components.GetLocalLastFiveArticles
{
    public class LocalLastFiveArticlesViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> Articles { get; set; }

        public Region? Region { get; set; }
    }
}
