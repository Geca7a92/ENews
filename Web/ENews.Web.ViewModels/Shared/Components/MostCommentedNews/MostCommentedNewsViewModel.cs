namespace ENews.Web.ViewModels.Shared.Components.MostCommentedNews
{
    using System.Collections.Generic;

    public class MostCommentedNewsViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> Articles { get; set; }
    }
}
