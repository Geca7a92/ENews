namespace ENews.Web.ViewModels.Shared.Components.LatestHeadlines
{
    using System.Collections.Generic;

    public class LatestHeadlinesViewModel
    {
        public IEnumerable<LatestHeadlineViewModel> Articles { get; set; }
    }
}
