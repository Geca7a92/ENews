namespace ENews.Web.ViewModels.Shared.Components.GetLocalLatest
{
    using System.Collections.Generic;

    using ENews.Data.Models.Enums;

    public class LocalLatestArticlesViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> Articles { get; set; }

        public Region? Region { get; set; }
    }
}
