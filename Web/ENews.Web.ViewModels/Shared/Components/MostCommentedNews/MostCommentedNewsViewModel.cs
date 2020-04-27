using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Shared.Components.MostCommentedNews
{
    public class MostCommentedNewsViewModel
    {
        public IEnumerable<ArticlePreviewViewModel> Articles { get; set; }
    }
}
